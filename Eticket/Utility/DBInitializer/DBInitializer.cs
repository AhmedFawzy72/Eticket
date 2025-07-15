using System.Threading.Tasks;
using Eticket.Data;
using Eticket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Eticket.Utility.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DBInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
            ,ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context= context;
        }
        public void Initialize()
        {
            try
            {

                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.SuperAdmin).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(SD.SuperAdmin)).GetAwaiter().GetResult();

                if (!_roleManager.RoleExistsAsync(SD.Admin).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();

                if (!_roleManager.RoleExistsAsync(SD.Customer).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();

                var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "SuperAdmin",
                        Email = "superadmin@gmail.com",
                        FirstName = "Super",
                        LastName = "Admin",
                        EmailConfirmed = true
                    };

                    var result = _userManager.CreateAsync(user, "Ab123456**").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, SD.SuperAdmin).GetAwaiter().GetResult();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
                else
                {
                    if (!_userManager.IsInRoleAsync(user, SD.SuperAdmin).GetAwaiter().GetResult())
                    {
                        _userManager.AddToRoleAsync(user, SD.SuperAdmin).GetAwaiter().GetResult();
                    }
                }




            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


    }
    }
}
