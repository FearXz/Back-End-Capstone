using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class ProdottoRistorante
    {
        // DB COLUMN
        [Key]
        public int IdProdottoRistorante { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        public string NomeProdotto { get; set; }

        [Required]
        public double PrezzoProdotto { get; set; }

        [Required]
        public bool IsAttivo { get; set; } = true;

        // NULLABLE
        public string? DescrizioneProdotto { get; set; }
        public string? ImgProdotto { get; set; }

        // NAVIGATION PROPERTY
        public virtual Ristorante Ristorante { get; set; }
        public virtual ICollection<ProdottiAcquistati> ProdottiAcquistati { get; set; }
        public virtual ICollection<IngredientiProdottoRistorante> IngredientiProdottoRistorante { get; set; }
        public virtual ICollection<ProdottoTipoProdotto> ProdottoTipoProdotti { get; set; }
    }
}
