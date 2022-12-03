using System.ComponentModel.DataAnnotations;

namespace FinalCIS174.Models.UserLogin
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        [StringLength(16)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(255)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
