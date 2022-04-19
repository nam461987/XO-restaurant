using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface ISliderBusiness
    {
        Task<Slider> Add(Slider model);
        Task<bool> Update(Slider model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<Slider>> GetAll(int accountTypeId, int pageIndex, int pageSize);
        Task<IEnumerable<Slider>> GetAllWithoutPaging(int accountTypeId);
        Task<Slider> GetById(int id);
    }
}
