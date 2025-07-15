using System.ComponentModel.DataAnnotations;

namespace Eticket.Models.ViewModels
{
    public class EditProfileVM
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string?LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

    }
}
