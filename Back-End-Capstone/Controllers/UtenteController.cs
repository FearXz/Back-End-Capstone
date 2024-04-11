using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Back_End_Capstone.Data;
using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_End_Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UtenteController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UtenteController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("getutente")]
        public IActionResult GetUtente()
        {
            string userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (userId == null)
            {
                return NotFound();
            }

            var utente = _db
                .Utenti.Select(u => new
                {
                    u.IdUtente,
                    u.Nome,
                    u.Cognome,
                    u.Email,
                    u.Cellulare,
                    u.Indirizzo,
                    u.Citta,
                    u.CAP,
                    u.Role,
                    Ordini = u
                        .Ordini.Where(o => o.IsPagato == true)
                        .Select(o => new
                        {
                            o.IdOrdini,
                            o.DataOrdine,
                            o.OrarioConsegnaPrevista,
                            o.Ristorante.NomeRistorante,
                            o.TotaleOrdine,
                            o.IsOrdineEvaso,
                            o.IsOrdineConsegnato,
                        })
                })
                .FirstOrDefault(u => u.IdUtente == Convert.ToInt32(userId));

            if (utente == null)
            {
                return NotFound();
            }

            return Ok(utente);
        }

        [HttpPut("updateutente")]
        public IActionResult UpdateUtente([FromBody] UtenteProfiloDto utente)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (userId == null)
            {
                return NotFound();
            }

            var utenteDb = _db.Utenti.FirstOrDefault(u => u.IdUtente == Convert.ToInt32(userId));

            if (utenteDb == null)
            {
                return NotFound();
            }

            utenteDb.Nome = utente.nome;
            utenteDb.Cognome = utente.cognome;
            utenteDb.Email = utente.email;
            utenteDb.Cellulare = utente.cellulare;
            utenteDb.Indirizzo = utente.indirizzo;
            utenteDb.Citta = utente.citta;
            utenteDb.CAP = utente.cap;

            if (
                utente.oldPassword != null
                && utente.newPassword != null
                && utente.confirmNewPassword != null
            )
            {
                //if (utente.oldPassword != utenteDb.Password)
                if (!BCrypt.Net.BCrypt.Verify(utente.oldPassword, utenteDb.Password))
                {
                    return BadRequest("La vecchia password non è corretta");
                }

                if (utente.newPassword != utente.confirmNewPassword)
                {
                    return BadRequest("Le nuove password non corrispondono");
                }

                utenteDb.Password = BCrypt.Net.BCrypt.HashPassword(utente.newPassword);
            }

            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("confirmOrder")]
        public IActionResult ConfirmOrder([FromBody] int idOrdine)
        {
            var ordine = _db.Ordini.FirstOrDefault(o => o.IdOrdini == idOrdine);

            if (ordine == null)
            {
                return NotFound();
            }

            ordine.IsOrdineConsegnato = true;

            _db.Ordini.Update(ordine);
            _db.SaveChanges();

            return Ok();
        }
    }
}
