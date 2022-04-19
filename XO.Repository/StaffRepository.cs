using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(XOContext context)
            : base(context)
        {
        }
    }
}
