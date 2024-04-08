using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Back_End_Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        [HttpPost("create-session")]
        public ActionResult CreateCheckoutSession([FromBody] CartOrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domain = "http://localhost:5173"; // URL del tuo frontend React

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

            return Ok(new { sessionId = session.Id });
        }

        [HttpGet("verify-session/{sessionId}")]
        public ActionResult VerifySession(string sessionId)
        {
            var service = new SessionService();
            Session session = service.Get(sessionId);

            if (session.PaymentStatus == "paid")
            {
                // Il pagamento è stato effettuato con successo
                return Ok();
            }
            else
            {
                // Il pagamento non è stato effettuato con successo
                return BadRequest();
            }
        }
    }
}
