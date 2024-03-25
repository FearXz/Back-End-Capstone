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

        // NULLABLE
        public string? DescrizioneProdotto { get; set; }
        public string? ImgProdotto { get; set; }

        // NAVIGATION PROPERTY
        public virtual Ristorante Ristorante { get; set; }
    }
}
