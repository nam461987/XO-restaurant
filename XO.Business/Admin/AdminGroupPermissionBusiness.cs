using XO.Business.Interfaces.Admin;
using XO.Common.Enums;
using XO.Common.Models;
using XO.Entities.ModelExtensions;
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
    public class AdminGroupPermissionBusiness : IAdminGroupPermissionBusiness
    {
        private readonly IAdminGroupPermissionRepository _adminGroupPermissionRepository;
        private readonly IAdminGroupRepository _adminGroupRepository;
        private readonly IAdminPermissionRepository _adminPermissionRepository;

        public AdminGroupPermissionBusiness(IMapper mapper,
            IAdminGroupPermissionRepository adminGroupPermissionRepository,
            IAdminGroupRepository adminGroupRepository,
            IAdminPermissionRepository adminPermissionRepository)
        {
            _adminGroupPermissionRepository = adminGroupPermissionRepository;
            _adminGroupRepository = adminGroupRepository;
            _adminPermissionRepository = adminPermissionRepository;
        }

        public async Task<List<AdminGroupPermission_View00>> GetPermissionByGroupAndModule(int groupId, string module)
        {
            var dbResults = new List<AdminGroupPermission_View00>();

            try
            {
                dbResults = await _adminGroupPermissionRepository.GetPermissionByGroupAndModule(groupId, module);
            }
            catch (Exception ex)
            {
                return null;
            }

            return dbResults;
        }
        public async Task<int> InsertOrUpdatePermission(int groupId, int permissionId, int status)
        {
            var dbResults = int.MinValue;

            try
            {
                dbResults = await _adminGroupPermissionRepository.InsertOrUpdatePermission(groupId, permissionId, status);
            }
            catch
            {
                dbResults = 0;
            }

            return dbResults;
        }
        public async Task<string[]> GetPermissionByGroup(int groupId)
        {
            var result = await _adminGroupPermissionRepository.GetPermissionByGroup(groupId);

            return result;
        }
        public async Task<List<AdminGroup>> GetGroup(int groupId)
        {

            var result = await _adminGroupRepository.Repo.Where(c => c.Id > (int)EAccountType.Admin).ToListAsync();

            return result;
        }
        public async Task<List<Option2Model>> GetModule()
        {
            var options = new List<Option2Model>();

            var result = await _adminPermissionRepository.Repo.Where(c => c.Status == (int)EStatus.Using).ToListAsync();

            if (result.Any())
            {
                options.AddRange(result.Select(c => c.Code).Distinct().Select(c => new Option2Model
                {
                    DisplayText = Convert.ToString(c),
                    Value = Convert.ToString(c)
                }).ToList());
            }

            return options;
        }
        public async Task<AdminGroupPermission> Add(AdminGroupPermission model)
        {
            var entity = _adminGroupPermissionRepository.Add(model);
            await _adminGroupPermissionRepository.SaveChangeAsync();
            model.Id = entity.Id;

            return model;
        }
    }
}
