using Back_End_Capstone.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class Ristorante
    {
        // DB COLUMN
        [Key]
        public int IdRistorante { get; set; }

        [Required]
        [ForeignKey("Azienda")]
        public int IdAzienda { get; set; }

        [Required]
        public string NomeRistorante { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        public string Latitudine { get; set; }

        [Required]
        public string Longitudine { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Role { get; set; } = UserRoles.AZIENDA;

        // NULLABLE
        public string? ImgCopertina { get; set; }
        public string? ImgLogo { get; set; }
        public string? Descrizione { get; set; }

        // NAVIGATION PROPERTY

        public virtual Azienda Azienda { get; set; }
        public virtual ICollection<ProdottoRistorante> ProdottiRistorante { get; set; }
        public virtual ICollection<IngredientiRistorante> IngredientiRistorante { get; set; }
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
