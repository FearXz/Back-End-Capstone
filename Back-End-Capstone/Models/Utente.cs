using Back_End_Capstone.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class Utente
    {
        [Key]
        public int IdUtente { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        public string CAP { get; set; }

        [Required]
        public string Cellulare { get; set; }

        [Required]
        public string Role { get; set; } = UserRoles.UTENTE;

        // NOT MAPPED

        [NotMapped]
        public string NomeCompleto => $"{Nome} {Cognome}";

        // NAVIGATION PROPERTIES
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
