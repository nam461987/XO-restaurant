using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;

namespace XO.Repository.Admin
{
    public class AdminGroupRepository : BaseRepository<AdminGroup>, IAdminGroupRepository
    {
        public AdminGroupRepository(XOContext context)
            : base(context)
        {
        }
    }
}
