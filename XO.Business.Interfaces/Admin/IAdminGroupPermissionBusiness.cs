using XO.Common.Models;
using XO.Entities.ModelExtensions;
using XO.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XO.Business.Interfaces.Admin
{
    public interface IAdminGroupPermissionBusiness
    {
        Task<List<AdminGroupPermission_View00>> GetPermissionByGroupAndModule(int groupId, string module);
        Task<int> InsertOrUpdatePermission(int groupId, int permissionId, int status);
        Task<string[]> GetPermissionByGroup(int groupId);
        Task<List<AdminGroup>> GetGroup(int groupId);
        Task<List<Option2Model>> GetModule();
        Task<AdminGroupPermission> Add(AdminGroupPermission model);
    }
}
