using System.ComponentModel.DataAnnotations;

namespace TesteCitel.API.Models
{
    public class AuthenticationViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; } = "admin";

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = "admin";

    }
}
