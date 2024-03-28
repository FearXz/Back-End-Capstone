using System.ComponentModel.DataAnnotations;

namespace Back_End_Capstone.Models
{
    public class Categorie
    {
        // DB COLUMN
        [Key]
        public int IdCategorie { get; set; }

        [Required]
        public string NomeCategoria { get; set; }

        // NAVIGATION PROPERTY
        public virtual ICollection<CategorieRistorante> CategorieRistorante { get; set; }
    }
}
