using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class Ordini
    {
        // DB COLUMN
        [Key]
        public int IdOrdini { get; set; }

        [Required]
        [ForeignKey("Utente")]
        public int IdUtente { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        public string IndirizzoConsegna { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; } = DateTime.Now;

        [Required]
        public TimeSpan OrarioConsegnaPrevista { get; set; }

        [Required]
        public bool IsOrdineEvaso { get; set; } = false;

        [Required]
        public bool IsOrdineConsegnato { get; set; } = false;

        // NOT REQUIRED - NULLABLE
        public string? Note { get; set; }

        // NOT MAPPED
        [NotMapped]
        public double PrezzoTotale { get; set; }

        // NAVIGATION PROPERTIES
        public virtual Utente Utente { get; set; }
        public virtual Ristorante Ristorante { get; set; }
        public virtual ICollection<ProdottiAcquistati> ProdottiAcquistati { get; set; }
    }
}
