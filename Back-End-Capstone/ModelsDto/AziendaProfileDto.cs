namespace Back_End_Capstone.ModelsDto
{
    public class AziendaProfileDto
    {
        public string nomeAzienda { get; set; }
        public string partitaIva { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string indirizzo { get; set; }
        public string citta { get; set; }
        public string cap { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmNewPassword { get; set; }
    }
}
