using System.ComponentModel.DataAnnotations;

namespace Eticket.Models.ViewModels
{
    public class LoginVm
    {
        public int Id { get; set; }
        [Required]
        public string EmailORUserName { set; get; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { set; get; } = string.Empty;
        public bool RememberMe { set; get; }
    }
}
