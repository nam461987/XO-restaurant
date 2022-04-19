using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class ServiceTypeRepository : BaseRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(XOContext context)
            : base(context)
        {
        }
    }
}
