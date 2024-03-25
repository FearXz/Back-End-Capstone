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

        [NotMapped]
        public string NomeCompleto => $"{Nome} {Cognome}";

        [Required]
        public string Cellulare { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
}
