using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.Service;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IServiceBusiness
    {
        Task<Service> Add(Service model);
        Task<bool> Update(Service model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<ServiceDto>> GetAll(int accountTypeId, int pageIndex, int pageSize);
        Task<List<ServiceDto>> GetByType(int accountTypeId, int type);
        Task<Service> GetById(int id);
    }
}
