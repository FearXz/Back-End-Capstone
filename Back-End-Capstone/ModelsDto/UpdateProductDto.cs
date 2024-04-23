namespace Back_End_Capstone.ModelsDto
{
    public class UpdateProductDto
    {
        public int IdRistorante { get; set; }
        public int IdProdottoRistorante { get; set; }
        public string NomeProdotto { get; set; }
        public double PrezzoProdotto { get; set; }
        public bool IsAttivo { get; set; }
        public string DescrizioneProdotto { get; set; }
        public int IdTipiProdotto { get; set; }
        public int[] IdIngredienti { get; set; }
    }
}
