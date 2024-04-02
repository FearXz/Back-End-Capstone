using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class IngredientiRistorante
    {
        // DB COLUMN
        [Key]
        public int IdIngrediente { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        public string NomeIngrediente { get; set; }

        [Required]
        public double PrezzoIngrediente { get; set; }

        [Required]
        public bool IsAttivo { get; set; } = true;

        // NOT MAPPED //NULLABLE

        // NAVIGATION PROPERTY
        public virtual Ristorante Ristorante { get; set; }
        public virtual ICollection<IngredientiProdottoRistorante> IngredientiProdottoRistorante { get; set; }
        public virtual ICollection<IngredientiProdottoAcquistato> IngredientiProdottiAcquistati { get; set; }
    }
}
