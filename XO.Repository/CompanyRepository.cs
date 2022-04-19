using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(XOContext context)
            : base(context)
        {
        }
    }
}
