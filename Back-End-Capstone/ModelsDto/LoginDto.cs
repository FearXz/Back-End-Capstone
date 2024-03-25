using System.ComponentModel.DataAnnotations;

namespace Back_End_Capstone.ModelsDto
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
