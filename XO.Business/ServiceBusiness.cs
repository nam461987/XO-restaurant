using XO.Business.Filter;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.Service;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XO.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBusiness(IMapper mapper,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Service> Add(Service model)
        {
            var entity = _serviceRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _serviceRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _serviceRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<ServiceDto>> GetAll(int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var ServiceRepo = _serviceRepository.Repo;

            var result = await (from service in ServiceRepo
                                select new ServiceDto
                                {
                                    Id = service.Id,
                                    ServiceTypeId = service.ServiceTypeId,
                                    ServiceTypeIdName = service.ServiceType.Name,
                                    ServiceTypeIdStatus = service.ServiceType.Status,
                                    SubServiceId = service.SubServiceId,
                                    Name = service.Name,
                                    Image = service.Image,
                                    Price = service.Price,
                                    Icon = service.Icon,
                                    Times = service.Times,
                                    Note = service.Note,
                                    Description = service.Description,
                                    Status = service.Status,
                                    CreatedStaffId = service.CreatedStaffId,
                                    CreatedDate = service.CreatedDate,
                                    UpdatedStaffId = service.UpdatedStaffId,
                                    UpdatedDate = service.UpdatedDate
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.ServiceTypeIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<List<ServiceDto>> GetByType(int accountTypeId, int type)
        {
            var ServiceRepo = _serviceRepository.Repo;

            var result = await (from service in ServiceRepo
                                select new ServiceDto
                                {
                                    Id = service.Id,
                                    ServiceTypeId = service.ServiceTypeId,
                                    ServiceTypeIdName = service.ServiceType.Name,
                                    ServiceTypeIdStatus = service.ServiceType.Status,
                                    SubServiceId = service.SubServiceId,
                                    Name = service.Name,
                                    Image = service.Image,
                                    Price = service.Price,
                                    Icon = service.Icon,
                                    Times = service.Times,
                                    Note = service.Note,
                                    Description = service.Description,
                                    Status = service.Status,
                                    CreatedStaffId = service.CreatedStaffId,
                                    CreatedDate = service.CreatedDate,
                                    UpdatedStaffId = service.UpdatedStaffId,
                                    UpdatedDate = service.UpdatedDate
                                })
                          .Where(c => c.ServiceTypeId == type && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.ServiceTypeIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToListAsync();

            return result;
        }

        public async Task<Service> GetById(int id)
        {
            var result = await _serviceRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(Service model)
        {
            var result = false;
            var record = await _serviceRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.ServiceTypeId = model.ServiceTypeId;
                record.SubServiceId = model.SubServiceId;
                record.Name = model.Name;
                record.Image = model.Image;
                record.Price = model.Price;
                record.Icon = model.Icon;
                record.Times = model.Times;
                record.Note = model.Note;
                record.Description = model.Description;
                record.UpdatedDate = model.UpdatedDate;
                record.UpdatedStaffId = model.UpdatedStaffId;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
