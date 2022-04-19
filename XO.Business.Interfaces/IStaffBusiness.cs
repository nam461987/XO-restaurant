using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.Staff;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IStaffBusiness
    {
        Task<Staff> Add(Staff model);
        Task<bool> Update(Staff model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<StaffDto>> GetAll(int userTypeId, int pageIndex, int pageSize);
        Task<IEnumerable<StaffDto>> GetAllWithoutPaging(int userTypeId);
        Task<StaffDto> GetAdminStaff(int userTypeId);
        Task<Staff> GetById(int id);
    }
}
