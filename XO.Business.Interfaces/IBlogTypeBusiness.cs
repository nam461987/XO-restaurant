using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IBlogTypeBusiness
    {
        Task<BlogType> Add(BlogType model);
        Task<bool> Update(BlogType model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<BlogType>> GetAll(int pageIndex, int pageSize);
        Task<IEnumerable<BlogType>> GetAllWithoutPaging(int accountTypeId);
        Task<BlogType> GetById(int id);
    }
}
