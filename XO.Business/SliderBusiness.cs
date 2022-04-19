using XO.Business.Filter;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XO.Business
{
    public class SliderBusiness : ISliderBusiness
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SliderBusiness(IMapper mapper,
            ISliderRepository sliderRepository,
            IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Slider> Add(Slider model)
        {
            var entity = _sliderRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _sliderRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _sliderRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<Slider>> GetAll(int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var result = await _sliderRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .ToFilterByAccountType(null,
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                .OrderBy(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<IEnumerable<Slider>> GetAllWithoutPaging(int accountTypeId)
        {
            var result = await _sliderRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .ToFilterByAccountType(null,
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                .OrderBy(c => c.Id)
                .ToListAsync();
            return result;
        }

        public async Task<Slider> GetById(int id)
        {
            var result = await _sliderRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(Slider model)
        {
            var result = false;
            var record = await _sliderRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Title = model.Title;
                record.Image = model.Image;
                record.Description = model.Description;
                record.Button = model.Button;
                record.ButtonUrl = model.ButtonUrl;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
