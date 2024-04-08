namespace Back_End_Capstone.ModelsDto
{
    public class CartOrderDto
    {
        public int idOrdini { get; set; }
        public string indirizzoDiConsegna { get; set; }
        public string orarioConsegnaPrevista { get; set; }
        public string note { get; set; }
        public double totale { get; set; }
        public ICollection<CartProductDto> prodotti { get; set; }
    }
}
