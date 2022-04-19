using XO.Business.Filter;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.Photo;
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
    public class PhotoBusiness : IPhotoBusiness
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoBusiness(IMapper mapper,
            IPhotoRepository photoRepository,
            IUnitOfWork unitOfWork)
        {
            _photoRepository = photoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Photo> Add(Photo model)
        {
            var entity = _photoRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _photoRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _photoRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<PhotoDto>> GetAll(int accountTypeId,
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var PhotoRepo = _photoRepository.Repo;

            var result = await (from photo in PhotoRepo
                                select new PhotoDto
                                {
                                    Id = photo.Id,
                                    ServiceId = photo.ServiceId,
                                    ServiceIdName = photo.Service.Name,
                                    ServiceIdStatus = photo.Service.Status,
                                    Name = photo.Name,
                                    Image = photo.Image,
                                    Status = photo.Status
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.ServiceIdStatus.GetValueOrDefault()
                          , f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);


            return result;
        }
        public async Task<IPaginatedList<PhotoDto>> GetByType(int accountTypeId, int type,
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var PhotoRepo = _photoRepository.Repo;

            var result = await (from photo in PhotoRepo
                                select new PhotoDto
                                {
                                    Id = photo.Id,
                                    ServiceId = photo.ServiceId,
                                    ServiceIdName = photo.Service.Name,
                                    ServiceIdStatus = photo.Service.Status,
                                    Name = photo.Name,
                                    Image = photo.Image,
                                    Status = photo.Status
                                })
                          .Where(c => c.ServiceId == type && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.ServiceIdStatus.GetValueOrDefault()
                          , f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }
        public async Task<List<PhotoDto>> GetAllWithoutPaging(int accountTypeId)
        {
            var PhotoRepo = _photoRepository.Repo;

            var result = await (from photo in PhotoRepo
                                select new PhotoDto
                                {
                                    Id = photo.Id,
                                    ServiceId = photo.ServiceId,
                                    ServiceIdName = photo.Service.Name,
                                    ServiceIdStatus = photo.Service.Status,
                                    Name = photo.Name,
                                    Image = photo.Image,
                                    Status = photo.Status
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.ServiceIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToListAsync();

            return result;
        }
        public async Task<Photo> GetById(int id)
        {
            var result = await _photoRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(Photo model)
        {
            var result = false;
            var record = await _photoRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.ServiceId = model.ServiceId;
                record.Name = model.Name;
                record.Image = model.Image;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
