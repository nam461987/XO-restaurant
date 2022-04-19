using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;

namespace XO.Repository.Admin
{
    public class AdminAccountRepository : BaseRepository<AdminAccount>, IAdminAccountRepository
    {
        public AdminAccountRepository(XOContext context)
            : base(context)
        {
        }
    }
}
