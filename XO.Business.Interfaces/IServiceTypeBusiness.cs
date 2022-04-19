using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IServiceTypeBusiness
    {
        Task<ServiceType> Add(ServiceType model);
        Task<bool> Update(ServiceType model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<ServiceType>> GetAll(int accountTypeId, int pageIndex, int pageSize);
        Task<ServiceType> GetById(int id);
    }
}
