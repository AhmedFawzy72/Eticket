using Microsoft.AspNetCore.Identity;

namespace Eticket.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { set; get; }=string.Empty;
        public string LastName { set; get; } = string.Empty;
        public string? Address { set; get; }
    }
}
