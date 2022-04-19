using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(XOContext context)
            : base(context)
        {
        }
    }
}
