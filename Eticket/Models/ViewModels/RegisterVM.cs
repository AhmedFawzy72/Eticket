using System.ComponentModel.DataAnnotations;

namespace Eticket.Models.ViewModels
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { set; get; } = string.Empty;
        [Required]
        public string LastName { set; get; } = string.Empty;
        [Required]
        public string UserName { set; get; } = string.Empty;
        [Required,DataType(DataType.EmailAddress)]
        public string Email { set; get; } = string.Empty;
        [Required,DataType(DataType.Password)]
        public string Password { set; get; } = string.Empty;
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { set; get; } = string.Empty;
        [Required]
        public string Phone { set; get; } = string.Empty;
        public string? Address { set; get; } 


    }
}
