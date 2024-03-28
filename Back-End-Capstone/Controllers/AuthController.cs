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
                            utente.Indirizzo,
                            utente.CAP,
                            utente.Role
                        }
                    }
                );
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationDto register)
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
