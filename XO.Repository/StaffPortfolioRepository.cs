using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class StaffPortfolioRepository : BaseRepository<StaffPortfolio>, IStaffPortfolioRepository
    {
        public StaffPortfolioRepository(XOContext context)
            : base(context)
        {
        }
    }
}
