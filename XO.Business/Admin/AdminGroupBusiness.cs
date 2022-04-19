using XO.Business.Interfaces.Admin;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO.Business.Admin
{
    public class AdminGroupBusiness : IAdminGroupBusiness
    {
        private readonly IAdminGroupRepository _adminGroupRepository;

        public AdminGroupBusiness(IMapper mapper, IAdminGroupRepository adminGroupRepository)
        {
            _adminGroupRepository = adminGroupRepository;
        }

        public async Task<AdminGroup> Add(AdminGroup model)
        {
            var entity = _adminGroupRepository.Add(model);
            await _adminGroupRepository.SaveChangeAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _adminGroupRepository.Delete(id);
            var recordUpdated = await _adminGroupRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _adminGroupRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _adminGroupRepository.SaveChangeAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<AdminGroup>> GetAll(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var result = await _adminGroupRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .OrderBy(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<AdminGroup> GetById(int id)
        {
            var result = await _adminGroupRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(AdminGroup model)
        {
            var result = false;
            var record = await _adminGroupRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Name = model.Name;
                record.Code = model.Code;
                record.Description = model.Description;
                record.UpdatedDate = model.UpdatedDate;
                record.UpdatedStaffId = model.UpdatedStaffId;

                await _adminGroupRepository.SaveChangeAsync();

                result = true;
            }
            return result;
        }
    }
}
