using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ElectroInternalControlSystem.Models
{
    public class ApplicationUser : IdentityUser //inherits the IdentityUser and add extra fields.
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}