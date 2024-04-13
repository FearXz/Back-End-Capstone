using System.ComponentModel.DataAnnotations;

namespace Back_End_Capstone.ModelsDto
{
    public class RegistrationAziendaDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NomeAzienda { get; set; }

        [Required]
        public string PartitaIva { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Citta { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        public string Telefono { get; set; }
    }
}
