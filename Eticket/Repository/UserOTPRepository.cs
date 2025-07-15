using Eticket.Data;
using Eticket.Models;
using Eticket.Repository.IRepositories;

namespace Eticket.Repository
{
    public class UserOTPRepository : Repository<UserOTP>, IUserOTPRepository
    {
        private readonly ApplicationDbContext _context;

        public UserOTPRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
    }
}
