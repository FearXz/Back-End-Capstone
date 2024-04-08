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
                    ristorante.TagRistorante,
                    CategorieRistorante = ristorante.CategorieRistorante.Select(cr => new
                    {
                        cr.IdCategorie,
                        cr.Categorie.NomeCategoria
                    }),
                    GiorniDiChiusura = ristorante.GiorniChiusuraRistorante.Select(g => new
                    {
                        g.GiorniChiusura.NomeGiorno,
                        g.GiorniChiusura.NumeroGiorno
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

        [HttpGet("localeId/{id}")]
        public IActionResult GetRistorante(int id)
        {
            var ristorante = _db
                .Ristoranti.Where(r => r.IdRistorante == id && r.IsAttivo == true)
                .Select(ristorante => new
                {
                    ristorante.IdAzienda,
                    ristorante.Azienda.PartitaIva,
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
                    ristorante.TagRistorante,
                    IngredientiRistorante = ristorante
                        .IngredientiRistorante.Where(state => state.IsAttivo == true)
                        .Select(ir => new
                        {
                            ir.IdIngrediente,
                            ir.NomeIngrediente,
                            ir.PrezzoIngrediente,
                        }),
                    CategorieRistorante = ristorante.CategorieRistorante.Select(cr => new
                    {
                        cr.IdCategorie,
                        cr.Categorie.NomeCategoria
                    }),
                    GiorniDiChiusura = ristorante.GiorniChiusuraRistorante.Select(g => new
                    {
                        g.GiorniChiusura.NomeGiorno,
                        g.GiorniChiusura.NumeroGiorno
                    }),
                    Prodotti = ristorante
                        .ProdottiRistorante.Where(state => state.IsAttivo == true)
                        .Select(pr => new
                        {
                            pr.IdProdottoRistorante,
                            pr.NomeProdotto,
                            pr.PrezzoProdotto,
                            pr.DescrizioneProdotto,
                            pr.ImgProdotto,
                            Ingredienti = pr
                                .IngredientiProdottoRistorante.Where(state =>
                                    state.IngredientiRistorante.IsAttivo
                                )
                                .Select(ip => new
                                {
                                    ip.IngredientiRistorante.IdIngrediente,
                                    ip.IngredientiRistorante.NomeIngrediente,
                                    ip.IngredientiRistorante.PrezzoIngrediente,
                                }),
                            TipiProdotto = pr.ProdottoTipoProdotti.Select(ptp => new
                            {
                                ptp.TipoProdotto.IdTipoProdotto,
                                ptp.TipoProdotto.NomeTipoProdotto,
                            }),
                        }),
                });

            return Ok(ristorante);
        }
    }
}
