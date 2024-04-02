using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class GiorniChiusuraRistorante
    {
        [Key]
        public int IdGiorniChiusuraRistorante { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        [ForeignKey("GiorniChiusura")]
        public int IdGiorniChiusura { get; set; }

        // NAVIGATION PROPERTY

        public virtual Ristorante Ristorante { get; set; }
        public virtual GiorniChiusura GiorniChiusura { get; set; }
    }
}
