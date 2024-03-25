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
        public string NomeProdotto { get; set; }

        [Required]
        public double PrezzoProdotto { get; set; }

        [Required]
        public int Quantita { get; set; } = 1;

        // NULLABLE
        public string? DescrizioneProdotto { get; set; }
        public string? ImgProdotto { get; set; }

        // NOT MAPPED
        [NotMapped]
        public double PrezzoTotale => PrezzoProdotto * Quantita;

        // NAVIGATION PROPERTIES
        public virtual Ordini Ordine { get; set; }
        public virtual ICollection<IngredientiProdottoAcquistato> IngredientiProdottoAcquistato { get; set; }
    }
}
