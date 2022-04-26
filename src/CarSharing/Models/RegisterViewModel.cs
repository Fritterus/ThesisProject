using System.ComponentModel.DataAnnotations;

namespace CarSharing.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmed password required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }
    }
}