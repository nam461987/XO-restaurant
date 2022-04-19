using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(XOContext context)
            : base(context)
        {
        }
    }
}
