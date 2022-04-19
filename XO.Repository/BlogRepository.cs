using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(XOContext context)
            : base(context)
        {
        }
    }
}
