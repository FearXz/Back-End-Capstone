namespace Back_End_Capstone.ModelsDto
{
    public class CartIngredientDto
    {
        public int idIngrediente { get; set; }
        public string nomeIngrediente { get; set; }
        public double prezzoIngrediente { get; set; }
        public int quantita { get; set; }
        public bool isExtra { get; set; }
    }
}
