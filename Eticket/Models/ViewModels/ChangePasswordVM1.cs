using System.ComponentModel.DataAnnotations;

namespace Eticket.Models.ViewModels
{
    public class ChangePasswordVM1
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }=string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }=string.Empty;

        [Required]
        [DataType(DataType.Password), Compare("NewPassword")]
        
        public string ConfirmNewPassword { get; set; }= string.Empty;
    }

}
