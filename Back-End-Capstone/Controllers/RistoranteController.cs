using Back_End_Capstone.Data;
using Microsoft.AspNetCore.Mvc;

namespace Back_End_Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RistoranteController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RistoranteController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("listaRistoranti")]
        public IActionResult GetRistoranti()
        {
            var ristoranti = _db
                .Ristoranti.Where(r => r.IsAttivo == true)
                .Select(ristorante => new
                {
                    ristorante.IdAzienda,
                    ristorante.IdRistorante,
                    ristorante.NomeRistorante,
                    ristorante.Indirizzo,
                    ristorante.Citta,
                    ristorante.CAP,
                    ristorante.Latitudine,
                    ristorante.Longitudine,
                    ristorante.Telefono,
                    ristorante.OrarioApertura,
                    ristorante.OrarioChiusura,
                    ristorante.ImgCopertina,
                    ristorante.ImgLogo,
                    ristorante.Descrizione,
                    CategorieRistorante = ristorante.CategorieRistorante.Select(cr => new
                    {
                        cr.IdCategorie,
                        cr.Categorie.NomeCategoria
                    }),
                    Distanza = "",
                });

            return Ok(ristoranti);
        }

        [HttpGet("listaCategorie")]
        public IActionResult GetCategorie()
        {
            var categorie = _db.Categorie.Select(c => new { c.IdCategorie, c.NomeCategoria });

            return Ok(categorie);
        }
    }
}
