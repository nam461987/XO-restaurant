using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.Blog;
using XO.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IBlogBusiness
    {
        Task<Blog> Add(Blog model);
        Task<bool> Update(Blog model);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<BlogDto>> GetAll(int userTypeId, int pageIndex, int pageSize);
        Task<IPaginatedList<BlogDto>> GetByTypeId(int typeId, int userTypeId, int pageIndex, int pageSize);
        Task<IEnumerable<BlogDto>> GetHotBlogs(int userTypeId);
        Task<IEnumerable<Blog>> GetPreAndNextBlog(int blogId);
        Task<IEnumerable<BlogDto>> GetTwoRandomItems(int userTypeId);
        Task<BlogDto> GetById(int id);
    }
}
