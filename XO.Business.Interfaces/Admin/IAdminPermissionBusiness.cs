using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces.Admin
{
    public interface IAdminPermissionBusiness
    {
        Task<AdminPermission> Add(AdminPermission model);
        Task<bool> Update(AdminPermission model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<AdminPermission>> GetAll(int pageIndex, int pageSize);
        Task<AdminPermission> GetById(int id);
    }
}
