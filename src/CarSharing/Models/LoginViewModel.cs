using System.ComponentModel.DataAnnotations;

namespace CarSharing.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}