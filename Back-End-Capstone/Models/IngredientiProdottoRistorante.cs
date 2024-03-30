using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class IngredientiProdottoRistorante
    {
        // DB COLUMN
        [Key]
        public int IdIngredientiProdottoRistorante { get; set; }

        [Required]
        [ForeignKey("ProdottoRistorante")]
        public int IdProdottoRistorante { get; set; }

        [Required]
        [ForeignKey("IngredientiRistorante")]
        public int IdIngredientiRistorante { get; set; }

        // NOT MAPPED //NULLABLE

        // NAVIGATION PROPERTY
        public virtual ProdottoRistorante ProdottoRistorante { get; set; }
        public virtual IngredientiRistorante IngredientiRistorante { get; set; }
    }
}
