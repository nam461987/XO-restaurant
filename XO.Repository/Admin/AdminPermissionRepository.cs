using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;

namespace XO.Repository.Admin
{
    public class AdminPermissionRepository : BaseRepository<AdminPermission>, IAdminPermissionRepository
    {
        public AdminPermissionRepository(XOContext context)
            : base(context)
        {
        }
    }
}
