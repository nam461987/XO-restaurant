using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class BlogTypeRepository : BaseRepository<BlogType>, IBlogTypeRepository
    {
        public BlogTypeRepository(XOContext context)
            : base(context)
        {
        }
    }
}
