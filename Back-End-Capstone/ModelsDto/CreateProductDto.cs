namespace Back_End_Capstone.ModelsDto
{
    public class CreateProductDto
    {
        public int IdRistorante { get; set; }
        public string NomeProdotto { get; set; }
        public double PrezzoProdotto { get; set; }
        public bool IsAttivo { get; set; } = true;
        public string? DescrizioneProdotto { get; set; }
        public int? IdTipiProdotto { get; set; }
        public int[]? IdIngredienti { get; set; }
    }
}
