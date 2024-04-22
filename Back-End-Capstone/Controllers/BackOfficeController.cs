using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Back_End_Capstone.Data;
using Back_End_Capstone.Models;
using Back_End_Capstone.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_End_Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AZIENDA)]
    public class BackOfficeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BackOfficeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("getristorantibyidazienda")]
        public IActionResult GetListaRistorantiById()
        {
            var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (IdAzienda == null)
            {
                return BadRequest();
            }

            var listaLocali = _db
                .Ristoranti.Where(r => r.IdAzienda == Convert.ToInt32(IdAzienda))
                .Select(ristorante => new
                {
                    ristorante.IdAzienda,
                    ristorante.IdRistorante,
                    ristorante.NomeRistorante,
                    ristorante.IsAttivo,
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
                });

            return Ok(listaLocali);
        }

        [HttpGet("getristorantebyid/{id}")]
        public IActionResult GetRistoranteById(int id)
        {
            var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (IdAzienda == null)
            {
                return BadRequest();
            }

            var ristorante = _db
                .Ristoranti.Where(r =>
                    r.IdAzienda == Convert.ToInt32(IdAzienda) && r.IdRistorante == id
                )
                .Select(ristorante => new
                {
                    ristorante.IdAzienda,
                    ristorante.Azienda.PartitaIva,
                    ristorante.IdRistorante,
                    ristorante.NomeRistorante,
                    ristorante.Indirizzo,
                    ristorante.Citta,
                    ristorante.CAP,
                    ristorante.IsAttivo,
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
                        g.GiorniChiusura.IdGiorniChiusura,
                        g.GiorniChiusura.NomeGiorno,
                        g.GiorniChiusura.NumeroGiorno
                    }),
                    Ordini = ristorante
                        .Ordini.Where(o => o.IsPagato == true)
                        .Select(o => new
                        {
                            o.IdOrdini,
                            o.DataOrdine,
                            o.OrarioConsegnaPrevista,
                            o.TotaleOrdine,
                            o.Note,
                            o.IsOrdineEvaso,
                            o.IsOrdineConsegnato,
                            o.IsPagato,
                            o.IsRefunded,
                            o.StripeSessionId,
                            o.PaymentIntentId,
                            o.IndirizzoConsegna,
                            Utente = new
                            {
                                o.Utente.IdUtente,
                                o.Utente.Nome,
                                o.Utente.Cognome,
                                o.Utente.Email,
                                o.Utente.Indirizzo,
                                o.Utente.Citta,
                                o.Utente.CAP,
                                o.Utente.Cellulare,
                                o.Utente.Role,
                            },
                            ProdottiAcquistati = o.ProdottiAcquistati.Select(pa => new
                            {
                                pa.ProdottoRistorante.IdProdottoRistorante,
                                pa.ProdottoRistorante.NomeProdotto,
                                pa.ProdottoRistorante.DescrizioneProdotto,
                                pa.ProdottoRistorante.PrezzoProdotto,
                                pa.ProdottoRistorante.ImgProdotto,
                                pa.ProdottoRistorante.IsAttivo,
                                pa.Quantita,
                                pa.PrezzoTotale,
                                CategoriaProdotto = pa.ProdottoRistorante.ProdottoTipoProdotti.Select(
                                    cp => new
                                    {
                                        cp.IdTipoProdotto,
                                        cp.TipoProdotto.NomeTipoProdotto,
                                    }
                                ),
                                IngredientiAcquistati = pa.IngredientiProdottoAcquistato.Select(
                                    ipa => new
                                    {
                                        ipa.IngredientiRistorante.IdIngrediente,
                                        ipa.IngredientiRistorante.NomeIngrediente,
                                        ipa.IngredientiRistorante.PrezzoIngrediente,
                                        ipa.IngredientiRistorante.IsAttivo,
                                        ipa.Quantita,
                                        ipa.IsExtra,
                                    }
                                ),
                            })
                        })
                });

            return Ok(ristorante);
        }

        [HttpPut("confirmevaso")]
        public IActionResult ConfirmEvaso([FromBody] ConfirmEvasoDto order)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ordine = _db.Ordini.FirstOrDefault(o =>
                    o.IdOrdini == order.IdOrder
                    && o.IdRistorante == order.IdRistorante
                    && o.Ristorante.Azienda.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ordine == null)
                {
                    return NotFound();
                }

                ordine.IsOrdineEvaso = true;

                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("localeditmainmodal")]
        public IActionResult LocalEditMainModal([FromBody] LocalMainModalEditDto editObj)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == editObj.idRistorante
                    && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                ristorante.NomeRistorante = editObj.ristorante;
                ristorante.Telefono = editObj.telefono;
                ristorante.Descrizione = editObj.descrizione;
                ristorante.TagRistorante = editObj.tag;
                ristorante.OrarioApertura = TimeSpan.Parse(editObj.oraApertura);
                ristorante.OrarioChiusura = TimeSpan.Parse(editObj.oraChiusura);
                ristorante.Indirizzo = editObj.indirizzo;
                ristorante.Citta = editObj.citta;
                ristorante.CAP = editObj.cap;
                ristorante.Latitudine = editObj.latitudine;
                ristorante.Longitudine = editObj.longitudine;

                _db.Ristoranti.Update(ristorante);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getgiornidichiusura")]
        public IActionResult GetGiorniChiusura()
        {
            var giorniChiusura = _db.GiorniChiusura.Select(g => new
            {
                g.IdGiorniChiusura,
                g.NomeGiorno,
                g.NumeroGiorno
            });

            return Ok(giorniChiusura);
        }

        [HttpGet("gettagcategories")]
        public IActionResult GetTagCategories()
        {
            var categorie = _db.Categorie.Select(c => new { c.IdCategorie, c.NomeCategoria });

            return Ok(categorie);
        }

        [HttpPut("putgiornichiusura")]
        public IActionResult PutGiorniChiusura([FromBody] GiorniChiusuraDto giorniChiusura)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == giorniChiusura.IdRistorante
                    && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                var giorniChiusuraRistorante = _db.GiorniChiusuraRistoranti.Where(g =>
                    g.IdRistorante == giorniChiusura.IdRistorante
                );
                if (giorniChiusuraRistorante.Any())
                {
                    _db.GiorniChiusuraRistoranti.RemoveRange(giorniChiusuraRistorante);
                }
                foreach (int IdGiorno in giorniChiusura.IdGiorniChiusura)
                {
                    var nuovoGiorno = new GiorniChiusuraRistorante
                    {
                        IdRistorante = giorniChiusura.IdRistorante,
                        IdGiorniChiusura = IdGiorno,
                    };

                    _db.GiorniChiusuraRistoranti.Add(nuovoGiorno);
                }
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("puttagcategories")]
        public IActionResult PutTagCategories([FromBody] TagCategorieDto tagC)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == tagC.IdRistorante && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                var categorieRistorante = _db.CategorieRistoranti.Where(cr =>
                    cr.IdRistorante == tagC.IdRistorante
                );
                if (categorieRistorante.Any())
                {
                    _db.CategorieRistoranti.RemoveRange(categorieRistorante);
                }
                foreach (int IdCategoria in tagC.IdTagCategoria)
                {
                    var nuovaCategoria = new CategorieRistorante
                    {
                        IdRistorante = tagC.IdRistorante,
                        IdCategorie = IdCategoria,
                    };

                    _db.CategorieRistoranti.Add(nuovaCategoria);
                }
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("updateLogo/{idRistorante}")]
        public IActionResult UpdateLogo(int idRistorante, [FromForm] IFormFile logo)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == idRistorante && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                var ImgUploader = new ImgUploader();

                if (!string.IsNullOrEmpty(ristorante.ImgLogo))
                {
                    ImgUploader.DeleteFile("images/logo", ristorante.ImgLogo);
                    ristorante.ImgLogo = null;
                }

                ristorante.ImgLogo = ImgUploader.UploadFile(logo, "images/logo", HttpContext);

                _db.Ristoranti.Update(ristorante);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("updatecopertina/{idRistorante}")]
        public IActionResult UpdateCopertina(int idRistorante, [FromForm] IFormFile copertina)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == idRistorante && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                var ImgUploader = new ImgUploader();

                if (!string.IsNullOrEmpty(ristorante.ImgCopertina))
                {
                    ImgUploader.DeleteFile("images/copertina", ristorante.ImgCopertina);
                    ristorante.ImgCopertina = null;
                }

                ristorante.ImgCopertina = ImgUploader.UploadFile(
                    copertina,
                    "images/copertina",
                    HttpContext
                );

                _db.Ristoranti.Update(ristorante);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("updatelocalstatus")]
        public IActionResult UpdateLocalStatus([FromBody] LocalStatusDto localStatus)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = _db.Ristoranti.FirstOrDefault(r =>
                    r.IdRistorante == localStatus.IdRistorante
                    && r.IdAzienda == Convert.ToInt32(IdAzienda)
                );

                if (ristorante == null)
                {
                    return NotFound();
                }

                ristorante.IsAttivo = localStatus.IsAttivo;

                _db.Ristoranti.Update(ristorante);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("newlocalpost")]
        public IActionResult NewLocalPost([FromBody] NewLocalDto newLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ristorante = new Ristorante
                {
                    IdAzienda = Convert.ToInt32(IdAzienda),
                    NomeRistorante = newLocal.NomeRistorante,
                    Telefono = newLocal.Telefono,
                    Descrizione = newLocal.Descrizione,
                    TagRistorante = newLocal.TagRistorante,
                    OrarioApertura = TimeSpan.Parse(newLocal.OraApertura),
                    OrarioChiusura = TimeSpan.Parse(newLocal.OraChiusura),
                    Indirizzo = newLocal.Indirizzo,
                    Citta = newLocal.Citta,
                    CAP = newLocal.Cap,
                    Latitudine = newLocal.Latitudine,
                    Longitudine = newLocal.Longitudine,
                    IsAttivo = true,
                };

                _db.Ristoranti.Add(ristorante);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getingredientiristorante/{idRistorante}")]
        public IActionResult GetIngredientiRistorante(int idRistorante)
        {
            var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (IdAzienda == null)
            {
                return BadRequest();
            }

            var ingredienti = _db
                .IngredientiRistoranti.Where(i => i.IdRistorante == idRistorante)
                .Select(i => new
                {
                    i.IdIngrediente,
                    i.NomeIngrediente,
                    i.PrezzoIngrediente,
                    i.IsAttivo,
                });

            return Ok(ingredienti);
        }

        [HttpPost("newingredientipost")]
        public IActionResult NewIngredientiPost([FromBody] NewIngredientiDto newIngredienti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ingrediente = new IngredientiRistorante
                {
                    IdRistorante = newIngredienti.LocaleId,
                    NomeIngrediente = newIngredienti.NomeIngrediente,
                    PrezzoIngrediente = newIngredienti.PrezzoIngrediente,
                    IsAttivo = newIngredienti.IsAttivo,
                };

                _db.IngredientiRistoranti.Add(ingrediente);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("updateingredienti")]
        public IActionResult UpdateIngredienti([FromBody] UpdateIngredientiDto updateIngredienti)
        {
            try
            {
                var IdAzienda = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (IdAzienda == null)
                {
                    return BadRequest();
                }

                var ingrediente = _db.IngredientiRistoranti.FirstOrDefault(i =>
                    i.IdIngrediente == updateIngredienti.IdIngrediente
                    && i.IdRistorante == updateIngredienti.LocaleId
                );

                if (ingrediente == null)
                {
                    return NotFound();
                }

                ingrediente.NomeIngrediente = updateIngredienti.NomeIngrediente;
                ingrediente.PrezzoIngrediente = updateIngredienti.PrezzoIngrediente;
                ingrediente.IsAttivo = updateIngredienti.IsAttivo;

                _db.IngredientiRistoranti.Update(ingrediente);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
