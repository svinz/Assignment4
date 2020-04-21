using System.ComponentModel.DataAnnotations;

namespace ElectroInternalControlSystem.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}