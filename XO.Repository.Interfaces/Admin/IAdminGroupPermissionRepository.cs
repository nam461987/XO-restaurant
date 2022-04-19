using XO.Entities.ModelExtensions;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Repository.Interfaces.Admin
{
    public interface IAdminGroupPermissionRepository : IRepository<AdminGroupPermission>
    {
        Task<List<AdminGroupPermission_View00>> GetPermissionByGroupAndModule(int groupId, string module);
        Task<int> InsertOrUpdatePermission(int groupId, int permissionId, int status);
        Task<string[]> GetPermissionByGroup(int groupId);
    }
}
