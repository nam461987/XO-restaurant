using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.Staff;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IStaffPortfolioBusiness
    {
        Task<StaffPortfolio> Add(StaffPortfolio model);
        Task<bool> Update(StaffPortfolio model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<StaffPortfolio>> GetAll(int userTypeId, int staffId, int pageIndex, int pageSize);
        Task<IEnumerable<StaffPortfolio>> GetAllWithoutPaging(int userTypeId, int staffId);
        Task<StaffPortfolio> GetById(int id);
    }
}
