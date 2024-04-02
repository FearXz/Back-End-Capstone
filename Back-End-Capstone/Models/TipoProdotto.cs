using System.ComponentModel.DataAnnotations;

namespace Back_End_Capstone.Models
{
    public class TipoProdotto
    {
        [Key]
        public int IdTipoProdotto { get; set; }

        [Required]
        public string NomeTipoProdotto { get; set; }

        // NAVIGATION PROPERTIES

        public virtual ICollection<ProdottoTipoProdotto> ProdottoTipoProdotti { get; set; }
    }
}
