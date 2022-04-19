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
using XO.Business.Filter;
using System.Collections.Generic;

namespace XO.Business
{
    public class StaffPortfolioBusiness : IStaffPortfolioBusiness
    {
        private readonly IMapper _mapper;
        private readonly IStaffPortfolioRepository _staffPortfolioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StaffPortfolioBusiness(IMapper mapper,
            IStaffPortfolioRepository staffPortfolioRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _staffPortfolioRepository = staffPortfolioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffPortfolio> Add(StaffPortfolio model)
        {
            var entity = _staffPortfolioRepository.Add(model);
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
            _staffPortfolioRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _staffPortfolioRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = (byte)(Active == 1 ? 0 : 1);
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<StaffPortfolio>> GetAll(int accountTypeId, int staffId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var StaffPortfolioRepo = _staffPortfolioRepository.Repo;

            var result = await StaffPortfolioRepo
                          .Where(c => c.StaffId == staffId && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<IEnumerable<StaffPortfolio>> GetAllWithoutPaging(int accountTypeId, int staffId)
        {
            var StaffPortfolioRepo = _staffPortfolioRepository.Repo;

            var result = await StaffPortfolioRepo
                          .Where(c => c.StaffId == staffId && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(null,
                          f => f.Status, accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToListAsync();

            return result;
        }

        public async Task<StaffPortfolio> GetById(int id)
        {
            var result = await _staffPortfolioRepository.Repo
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Update(StaffPortfolio model)
        {
            var result = false;
            var record = await _staffPortfolioRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

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
