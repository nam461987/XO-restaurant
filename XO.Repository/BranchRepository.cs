using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(XOContext context)
            : base(context)
        {
        }
    }
}
