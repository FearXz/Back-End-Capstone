using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Back_End_Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "/success",
                CancelUrl = domain + "/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
    }
}
