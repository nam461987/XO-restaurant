using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XO.Business
{
    public class BranchBusiness : IBranchBusiness
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BranchBusiness(IMapper mapper,
            IBranchRepository branchRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Branch> Add(Branch model)
        {
            var entity = _branchRepository.Add(model);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _branchRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _branchRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<Branch>> GetAll(int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var result = await _branchRepository.Repo
                          .Where(c => c.Status < (int)EStatus.All)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<Branch> GetById(int id)
        {
            var result = await _branchRepository.Repo
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Update(Branch model)
        {
            var result = false;
            var record = await _branchRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Name = model.Name;
                record.Address = model.Address;
                record.Phone = model.Phone;
                record.Image = model.Image;
                record.AppointmentUrl = model.AppointmentUrl;
                record.Email = model.Email;
                record.Lat = model.Lat;
                record.Long = model.Long;
                record.UpdatedDate = model.UpdatedDate;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
