using System.ComponentModel.DataAnnotations;

namespace FinalCIS174.Models.UserLogin
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        [RegularExpression("^[a-zA-Z0-9_]+( [a-zA-Z0-9_]+)*$",
    ErrorMessage = "The username cannot have special characters")]
        [StringLength(16,
            ErrorMessage = " your username must be less then 16 characters.")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(255)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
