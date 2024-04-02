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
        [ForeignKey("IngredientiRistorante")]
        public int IdIngredientiRistorante { get; set; }

        [Required]
        public int Quantita { get; set; } = 1;

        [Required]
        public bool IsExtra { get; set; } = false;

        // NAVIGATION PROPERTIES
        public virtual ProdottiAcquistati ProdottoAcquistato { get; set; }
        public virtual IngredientiRistorante IngredientiRistorante { get; set; }
    }
}
