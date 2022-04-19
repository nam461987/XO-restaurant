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

namespace XO.Business
{
    public class ServiceTypeBusiness : IServiceTypeBusiness
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceTypeBusiness(IMapper mapper,
            IServiceTypeRepository serviceTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceType> Add(ServiceType model)
        {
            var entity = _serviceTypeRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _serviceTypeRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _serviceTypeRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<ServiceType>> GetAll(int accountTypeId, 
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var result = await _serviceTypeRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .ToFilterByAccountType(null, f => f.Status.GetValueOrDefault(), accountTypeId)
                .OrderBy(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<ServiceType> GetById(int id)
        {
            var result = await _serviceTypeRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(ServiceType model)
        {
            var result = false;
            var record = await _serviceTypeRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Name = model.Name;
                record.Image = model.Image;
                record.Icon = model.Icon;
                record.IconBackground = model.IconBackground;
                record.Description = model.Description;
                record.ServiceContent = model.ServiceContent;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
