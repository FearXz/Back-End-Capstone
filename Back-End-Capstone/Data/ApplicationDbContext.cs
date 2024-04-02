using Back_End_Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Capstone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<IngredientiProdottoRistorante>()
                .HasOne(i => i.ProdottoRistorante)
                .WithMany(p => p.IngredientiProdottoRistorante)
                .HasForeignKey(i => i.IdProdottoRistorante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<IngredientiProdottoRistorante>()
                .HasOne(i => i.IngredientiRistorante)
                .WithMany(i => i.IngredientiProdottoRistorante)
                .HasForeignKey(i => i.IdIngredientiRistorante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<IngredientiProdottoAcquistato>()
                .HasOne(i => i.IngredientiRistorante)
                .WithMany(i => i.IngredientiProdottiAcquistati)
                .HasForeignKey(i => i.IdIngredientiRistorante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<ProdottiAcquistati>()
                .HasOne(p => p.ProdottoRistorante)
                .WithMany(p => p.ProdottiAcquistati)
                .HasForeignKey(p => p.IdProdottoRistorante)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // Aggiungi le tabelle con DbSet

        //clinica
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Azienda> Aziende { get; set; }
        public DbSet<Ristorante> Ristoranti { get; set; }
        public DbSet<GiorniChiusura> GiorniChiusura { get; set; }
        public DbSet<GiorniChiusuraRistorante> GiorniChiusuraRistoranti { get; set; }
        public DbSet<CategorieRistorante> CategorieRistoranti { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<ProdottoRistorante> ProdottiRistoranti { get; set; }
        public DbSet<TipoProdotto> TipiProdotti { get; set; }
        public DbSet<ProdottoTipoProdotto> ProdottiTipiProdotti { get; set; }
        public DbSet<IngredientiRistorante> IngredientiRistoranti { get; set; }
        public DbSet<IngredientiProdottoRistorante> IngredientiProdottiRistoranti { get; set; }
        public DbSet<Ordini> Ordini { get; set; }
        public DbSet<ProdottiAcquistati> ProdottiAcquistati { get; set; }
        public DbSet<IngredientiProdottoAcquistato> IngredientiProdottiAcquistati { get; set; }
    }
}
