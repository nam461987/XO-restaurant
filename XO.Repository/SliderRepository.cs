using XO.Entities.Models;
using XO.Repository.Interfaces;

namespace XO.Repository
{
    public class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {
        public SliderRepository(XOContext context)
            : base(context)
        {
        }
    }
}
