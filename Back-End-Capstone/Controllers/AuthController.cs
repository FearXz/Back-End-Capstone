using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Back_End_Capstone.Data;
using Back_End_Capstone.Models;
using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_End_Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public AuthController(IConfiguration configuration, ApplicationDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var utente = Authenticate(login);

            if (utente != null)
            {
                var tokenString = BuildToken(utente);

                return Ok(
                    new
                    {
                        token = tokenString,
                        Utente = new
                        {
                            utente.IdUtente,
                            utente.Nome,
                            utente.Cognome,
                            utente.Email,
                            utente.Citta,
                            utente.Cellulare,
                            utente.Indirizzo,
                            utente.CAP,
                            utente.Role
                        }
                    }
                );
            }

            return Unauthorized();
        }

        [HttpPost("loginazienda")]
        public IActionResult Loginazienda([FromBody] LoginDto login)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var azienda = _db.Aziende.FirstOrDefault(u => u.Email == login.Email);

            if (azienda == null || !BCrypt.Net.BCrypt.Verify(login.Password, azienda.Password))
            {
                return Unauthorized();
            }

            if (azienda != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, azienda.IdAzienda.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, azienda.Email),
                    new Claim(ClaimTypes.Role, azienda.Role),
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(
                    new
                    {
                        token = tokenString,
                        Azienda = new
                        {
                            azienda.IdAzienda,
                            azienda.NomeAzienda,
                            azienda.PartitaIva,
                            azienda.Email,
                            azienda.Citta,
                            azienda.Telefono,
                            azienda.Indirizzo,
                            azienda.CAP,
                            azienda.Role
                        }
                    }
                );
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto register)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            // voglio controllare se prima di inserire un utente, se esiste già un utente con la stessa email
            var existingUser = _db.Utenti.FirstOrDefault(u => u.Email == register.Email);
            if (existingUser != null)
            {
                return Conflict();
            }

            var user = new Utente
            {
                Nome = register.Nome,
                Cognome = register.Cognome,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password).ToString(),
                Cellulare = register.Cellulare,
                Indirizzo = register.Indirizzo,
                Citta = register.Citta,
                CAP = register.CAP,
            };

            _db.Utenti.Add(user);
            _db.SaveChanges();

            var email = user.Email;
            var subject = $"Registrazione Effettuata con successo";
            var htmlMessage =
                $@"
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Document</title>
    <style>
      .text-center {{
        text-align: center;
      }}
    </style>
  </head>
  <body>
    <h1 class=""text-center"">CIAO {user.Nome}, BENVENUTO SU TAKE2ME</h1>
    <p class=""text-center"">Adesso puoi cominciare ad ordinare</p>
  </body>
</html>";

            EmailSender EmailSender = new EmailSender();
            await EmailSender.SendEmailAsync(email, subject, htmlMessage);

            return Ok();
        }

        [HttpPost("registerazienda")]
        public async Task<IActionResult> RegisterAzienda([FromBody] RegistrationAziendaDto register)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            // voglio controllare se prima di inserire un utente, se esiste già un utente con la stessa email o partita iva
            var existingUser = _db.Aziende.FirstOrDefault(u =>
                u.Email == register.Email || u.PartitaIva == register.PartitaIva
            );

            if (existingUser != null)
            {
                return Conflict();
            }

            var azienda = new Azienda
            {
                NomeAzienda = register.NomeAzienda,
                PartitaIva = register.PartitaIva,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password).ToString(),
                Telefono = register.Telefono,
                Indirizzo = register.Indirizzo,
                Citta = register.Citta,
                CAP = register.CAP,
            };

            _db.Aziende.Add(azienda);
            _db.SaveChanges();

            var email = azienda.Email;
            var subject = $"Registrazione Effettuata con successo";
            var htmlMessage =
                $@"
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Document</title>
    <style>
      .text-center {{
        text-align: center;
      }}
    </style>
  </head>
  <body>
    <h1 class=""text-center"">CIAO {azienda.NomeAzienda}, BENVENUTO SU TAKE2ME</h1>
    <p class=""text-center"">Adesso puoi creare il tuo locale</p>
  </body>
</html>";

            EmailSender EmailSender = new EmailSender();
            await EmailSender.SendEmailAsync(email, subject, htmlMessage);

            return Ok();
        }

        private string BuildToken(Utente utente)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, utente.IdUtente.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, utente.Email),
                new Claim(ClaimTypes.Role, utente.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Utente Authenticate(LoginDto login)
        {
            var user = _db.Utenti.FirstOrDefault(u => u.Email == login.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return null;
            }

            return new Utente
            {
                IdUtente = user.IdUtente,
                Email = user.Email,
                Nome = user.Nome,
                Cognome = user.Cognome,
                Role = user.Role,
                Indirizzo = user.Indirizzo,
                Citta = user.Citta,
                CAP = user.CAP,
            };
        }
    }
}
