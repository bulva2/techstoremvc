using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(64, ErrorMessage = "Překročena maximální délka jména!")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginViewModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginViewModel()
        {
            Username = null!;
            Password = null!;
        }
    }
}
