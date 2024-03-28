using Back_End_Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Capstone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Aggiungi le tabelle con DbSet

        //clinica
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Azienda> Aziende { get; set; }
        public DbSet<Ristorante> Ristoranti { get; set; }
        public DbSet<CategorieRistorante> CategorieRistoranti { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<ProdottoRistorante> ProdottiRistoranti { get; set; }
        public DbSet<IngredientiRistorante> IngredientiRistoranti { get; set; }
        public DbSet<IngredientiProdottoRistorante> IngredientiProdottiRistoranti { get; set; }
        public DbSet<Ordini> Ordini { get; set; }
        public DbSet<ProdottiAcquistati> ProdottiAcquistati { get; set; }
        public DbSet<IngredientiProdottoAcquistato> IngredientiProdottiAcquistati { get; set; }
    }
}
