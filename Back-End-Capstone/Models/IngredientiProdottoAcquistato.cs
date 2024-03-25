using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class IngredientiProdottoAcquistato
    {
        // DB COLUMN
        [Key]
        public int IdIngredientiProdottoAcquistato { get; set; }

        [Required]
        [ForeignKey("ProdottoAcquistato")]
        public int IdProdottoAcquistato { get; set; }

        [Required]
        public string NomeIngrediente { get; set; }

        [Required]
        public double PrezzoIngrediente { get; set; }

        // NAVIGATION PROPERTIES
        public virtual ProdottiAcquistati ProdottoAcquistato { get; set; }
    }
}
