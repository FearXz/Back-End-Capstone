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
    }
}
