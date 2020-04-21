using System.ComponentModel.DataAnnotations;
namespace ElectroInternalControlSystem.Models
{
    public class EditUserModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
