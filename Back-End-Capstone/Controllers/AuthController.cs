using Back_End_Capstone.Data;
using Back_End_Capstone.Models;
using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpPost("token")]
        public IActionResult CreateToken([FromBody] LoginDto login)
        {
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
                            utente.Cellulare,
                            utente.Role
                        }
                    }
                );
            }

            return Unauthorized();
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
            var user = _db.Utenti.FirstOrDefault(u =>
                u.Email == login.Email && u.Password == login.Password
            );

            if (user == null)
            {
                return null;
            }

            return new Utente
            {
                IdUtente = user.IdUtente,
                Email = user.Email,
                Nome = user.Nome,
                Cognome = user.Cognome,
                Cellulare = user.Cellulare,
                Role = user.Role,
            };
        }
    }
}
