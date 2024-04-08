using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class ProdottiAcquistati
    {
        // DB COLUMN
        [Key]
        public int IdProdottiAcquistati { get; set; }

        [Required]
        [ForeignKey("Ordine")]
        public int IdOrdine { get; set; }

        [Required]
        [ForeignKey("ProdottoRistorante")]
        public int IdProdottoRistorante { get; set; }

        [Required]
        public int Quantita { get; set; } = 1;

        // NULLABLE
        public double? PrezzoTotale { get; set; }

        // NAVIGATION PROPERTIES
        public virtual Ordini Ordine { get; set; }
        public virtual ProdottoRistorante ProdottoRistorante { get; set; }
        public virtual ICollection<IngredientiProdottoAcquistato> IngredientiProdottoAcquistato { get; set; }
    }
}
