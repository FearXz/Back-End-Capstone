namespace Back_End_Capstone.ModelsDto
{
    public class CartProductDto
    {
        public int idProdotto { get; set; }
        public string uniqueId { get; set; }
        public string nomeProdotto { get; set; }
        public double prezzo { get; set; }
        public int quantita { get; set; }
        public double? totale { get; set; }
        public ICollection<CartIngredientDto> ingredienti { get; set; }
    }
}
