using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class ProdottoTipoProdotto
    {
        [Key]
        public int IdProdottoTipoProdotto { get; set; }

        [Required]
        [ForeignKey("ProdottoRistorante")]
        public int IdProdottoRistorante { get; set; }

        [Required]
        [ForeignKey("TipoProdotto")]
        public int IdTipoProdotto { get; set; }

        public virtual TipoProdotto TipoProdotto { get; set; }
        public virtual ProdottoRistorante ProdottoRistorante { get; set; }
    }
}
