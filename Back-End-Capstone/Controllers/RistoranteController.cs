﻿using Back_End_Capstone.Data;
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
        public IActionResult GetRistoranti([FromQuery] string lat, [FromQuery] string lon)
        {
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
            {
                return BadRequest("Latitudine e longitudine sono obbligatori");
            }

            var ristoranti = _db.Ristoranti.Select(ristorante => new
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
    }
}
