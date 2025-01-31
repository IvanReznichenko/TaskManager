using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
