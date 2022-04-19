using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IBranchBusiness
    {
        Task<Branch> Add(Branch model);
        Task<bool> Update(Branch model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<Branch>> GetAll(int userTypeId, int pageIndex, int pageSize);
        Task<Branch> GetById(int id);
    }
}
