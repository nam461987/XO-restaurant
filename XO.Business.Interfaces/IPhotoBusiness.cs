using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.Photo;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IPhotoBusiness
    {
        Task<Photo> Add(Photo model);
        Task<bool> Update(Photo model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<PhotoDto>> GetAll(int accountTypeId, int pageIndex, int pageSize);
        Task<IPaginatedList<PhotoDto>> GetByType(int accountTypeId, int type, int pageIndex, int pageSize);
        Task<List<PhotoDto>> GetAllWithoutPaging(int accountTypeId);
        Task<Photo> GetById(int id);
    }
}
