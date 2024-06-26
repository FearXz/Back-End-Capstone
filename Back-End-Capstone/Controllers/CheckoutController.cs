﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Back_End_Capstone.Data;
using Back_End_Capstone.Models;
using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace Back_End_Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public CheckoutController(
            ApplicationDbContext db,
            IConfiguration configuration,
            IEmailSender emailSender
        )
        {
            _db = db;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpPost("create-session")]
        public ActionResult CreateCheckoutSession([FromBody] CartOrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ristorante = _db
                .Ristoranti.Where(r => r.IdRistorante == request.idRistorante && r.IsAttivo)
                .FirstOrDefault();

            if (ristorante == null)
            {
                return NotFound();
            }

            // Crea la lista degli articoli per la sessione di checkout
            var lineItems = request
                .prodotti.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.nomeProdotto,
                        },
                        UnitAmountDecimal = (long)(item.totale * 100), // Converti il prezzo in centesimi
                    },
                    Quantity = item.quantita, // Modifica la quantità se necessario
                })
                .ToList();

            // URL del tuo frontend React
            var domain = ClientInfo.DOMAIN;

            // devo recuperare l'id dell'utente loggato dal token JWT
            string userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            string email = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            // Crea opzioni per la sessione di checkout
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card", "paypal" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/checkout",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // Creo un nuovo ordine
            var order = new Ordini
            {
                IdUtente = Convert.ToInt32(userId),
                IdRistorante = request.idRistorante,
                IndirizzoConsegna = request.indirizzoDiConsegna,
                OrarioConsegnaPrevista = TimeSpan.Parse(request.orarioConsegnaPrevista),
                StripeSessionId = session.Id,
                Note = request.note,
                TotaleOrdine = request.totale,
                ProdottiAcquistati = request
                    .prodotti.Select(item => new ProdottiAcquistati
                    {
                        IdProdottoRistorante = item.idProdotto,
                        Quantita = item.quantita,
                        PrezzoTotale = item.totale,
                        IngredientiProdottoAcquistato = item
                            .ingredienti.Select(ingrediente => new IngredientiProdottoAcquistato
                            {
                                IdIngredientiRistorante = ingrediente.idIngrediente,
                                Quantita = ingrediente.quantita,
                                IsExtra = ingrediente.isExtra,
                            })
                            .ToList(),
                    })
                    .ToList(),
            };

            _db.Ordini.Add(order);
            _db.SaveChanges();

            return Ok(new { sessionId = session.Id });
        }

        // stripe listen --forward-to https://localhost:7194/Checkout/webhook
        // stripe trigger payment_intent.succeeded

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignatureHeader = Request.Headers["Stripe-Signature"];
            var secret = _configuration.GetSection("MySecrets")["WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, stripeSignatureHeader, secret);

                // Gestisci l'evento
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    //var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    //var metadata = paymentIntent.Metadata;
                    var session = stripeEvent.Data.Object as Session;
                    var sessionId = session.Id;
                    var paymentIntentId = session.PaymentIntentId;

                    // Cerca l'ordine nel database utilizzando l'ID della sessione di Stripe
                    var order = _db
                        .Ordini.Include(o => o.Utente)
                        .FirstOrDefault(o => o.StripeSessionId == sessionId);

                    if (order != null)
                    {
                        // Aggiorna lo stato dell'ordine a isPagato = true e salva l'ID dell'intento di pagamento
                        order.IsPagato = true;
                        order.PaymentIntentId = paymentIntentId;
                        _db.Ordini.Update(order);
                        _db.SaveChanges();

                        // Invia una mail di conferma all'utente

                        var email = order.Utente.Email;
                        var subject = $"Ordine No. {order.IdOrdini} confermato";
                        var htmlMessage = "<h1>Il tuo ordine è stato confermato</h1>";

                        await _emailSender.SendEmailAsync(email, subject, htmlMessage);
                    }
                }

                return new EmptyResult();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        [HttpPut("verify-session/{sessionId}")]
        public ActionResult VerifySession(string sessionId)
        {
            var service = new SessionService();
            Session session = service.Get(sessionId);

            var order = _db.Ordini.FirstOrDefault(o => o.StripeSessionId == sessionId);

            if (order == null)
            {
                return NotFound();
            }

            if (session.PaymentStatus == "paid")
            {
                if (order.IsPagato)
                {
                    return Ok();
                }
                else
                {
                    order.IsPagato = true;
                    _db.Ordini.Update(order);
                    _db.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                // cancella l'ordine
                // _db.Ordini.Remove(order);
                // _db.SaveChanges();

                // Il pagamento non è stato effettuato con successo
                return NotFound();
            }
        }
    }
}
