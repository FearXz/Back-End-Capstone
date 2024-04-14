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
    [Authorize(Roles = UserRoles.AZIENDA)]
    public class AziendaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AziendaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("getazienda")]
        public IActionResult GetAzienda()
        {
            string IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (IdAzienda == null)
            {
                return NotFound();
            }

            var azienda = _db
                .Aziende.Select(a => new
                {
                    a.IdAzienda,
                    a.NomeAzienda,
                    a.PartitaIva,
                    a.Email,
                    a.Telefono,
                    a.Indirizzo,
                    a.Citta,
                    a.CAP,
                    a.Role,
                })
                .FirstOrDefault(a => a.IdAzienda == Convert.ToInt32(IdAzienda));

            if (azienda == null)
            {
                return NotFound();
            }

            return Ok(azienda);
        }

        [HttpPut("updateazienda")]
        public IActionResult UpdateAzienda(AziendaProfileDto azienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (IdAzienda == null)
            {
                return NotFound();
            }

            var aziendaDb = _db.Aziende.FirstOrDefault(a =>
                a.IdAzienda == Convert.ToInt32(IdAzienda)
            );

            if (aziendaDb == null)
            {
                return NotFound();
            }

            aziendaDb.NomeAzienda = azienda.nomeAzienda;
            aziendaDb.PartitaIva = azienda.partitaIva;
            aziendaDb.Email = azienda.email;
            aziendaDb.Telefono = azienda.telefono;
            aziendaDb.Indirizzo = azienda.indirizzo;
            aziendaDb.Citta = azienda.citta;
            aziendaDb.CAP = azienda.cap;

            if (
                azienda.oldPassword != null
                && azienda.newPassword != null
                && azienda.confirmNewPassword != null
            )
            {
                //if (utente.oldPassword != utenteDb.Password)
                if (!BCrypt.Net.BCrypt.Verify(azienda.oldPassword, aziendaDb.Password))
                {
                    return BadRequest("La vecchia password non è corretta");
                }

                if (azienda.newPassword != azienda.confirmNewPassword)
                {
                    return BadRequest("Le nuove password non corrispondono");
                }
                aziendaDb.Password = BCrypt.Net.BCrypt.HashPassword(azienda.newPassword);
            }

            _db.Aziende.Update(aziendaDb);
            _db.SaveChanges();

            return Ok();
        }
    }
}
