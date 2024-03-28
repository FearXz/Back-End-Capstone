using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class CategorieRistorante
    {
        // DB COLUMN
        [Key]
        public int IdCategorieRistorante { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        [ForeignKey("Categorie")]
        public int IdCategorie { get; set; }

        // NAVIGATION PROPERTIES
        public virtual Ristorante Ristorante { get; set; }
        public virtual Categorie Categorie { get; set; }
    }
}
