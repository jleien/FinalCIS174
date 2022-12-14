using System.ComponentModel.DataAnnotations;

namespace FinalCIS174.Models.UserLogin
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [RegularExpression("^[a-zA-Z0-9_]+( [a-zA-Z0-9_]+)*$",
    ErrorMessage = "The username cannot have special characters")]
        [StringLength(16)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
