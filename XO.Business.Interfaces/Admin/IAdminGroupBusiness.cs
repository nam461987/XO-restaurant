using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces.Admin
{
    public interface IAdminGroupBusiness
    {
        Task<AdminGroup> Add(AdminGroup model);
        Task<bool> Update(AdminGroup model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<AdminGroup>> GetAll(int pageIndex, int pageSize);
        Task<AdminGroup> GetById(int id);
    }
}
