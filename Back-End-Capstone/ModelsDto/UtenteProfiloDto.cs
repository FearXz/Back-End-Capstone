namespace Back_End_Capstone.ModelsDto
{
    public class UtenteProfiloDto
    {
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }
        public string cellulare { get; set; }
        public string indirizzo { get; set; }
        public string citta { get; set; }
        public string cap { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmNewPassword { get; set; }
    }
}
