namespace Back_End_Capstone.ModelsDto
{
    public class UpdateIngredientiDto
    {
        public int IdIngrediente { get; set; }
        public int LocaleId { get; set; }
        public string NomeIngrediente { get; set; }
        public double PrezzoIngrediente { get; set; }
        public bool IsAttivo { get; set; }
    }
}
