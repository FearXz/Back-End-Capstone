using System.ComponentModel.DataAnnotations;

namespace Back_End_Capstone.Models
{
    public class GiorniChiusura
    {
        [Key]
        public int IdGiorniChiusura { get; set; }

        [Required]
        public int NumeroGiorno { get; set; }

        [Required]
        public string NomeGiorno { get; set; }

        // NAVIGATION PROPERTY

        public virtual ICollection<GiorniChiusuraRistorante> GiorniChiusuraRistorante { get; set; }
    }
}
