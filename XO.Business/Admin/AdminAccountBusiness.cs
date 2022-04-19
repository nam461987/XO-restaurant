using XO.Business.Filter;
using XO.Business.Interfaces.Admin;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Enums;
using XO.Common.Responses.AdminAccount;
using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XO.Business.Admin
{
    public class AdminAccountBusiness : IAdminAccountBusiness
    {
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;
        private readonly IAdminAccountRepository _adminAccountRepository;
        private readonly IAdminGroupRepository _adminGroupRepository;
        private readonly IAdminPermissionRepository _adminPermissionRepository;
        private readonly IAdminGroupPermissionRepository _adminGroupPermissionRepository;

        public AdminAccountBusiness(IMapper mapper,
            IDataProtectionProvider provider,
            IAdminAccountRepository adminAccountRepository,
            IAdminGroupRepository adminGroupRepository,
            IAdminPermissionRepository adminPermissionRepository,
            IAdminGroupPermissionRepository adminGroupPermissionRepository)
        {
            _mapper = mapper;
            _protector = provider.CreateProtector("Authorize");
            _adminAccountRepository = adminAccountRepository;
            _adminGroupRepository = adminGroupRepository;
            _adminPermissionRepository = adminPermissionRepository;
            _adminGroupPermissionRepository = adminGroupPermissionRepository;
        }

        public async Task<AdminAccount> Add(AdminAccount model)
        {
            var entity = _adminAccountRepository.Add(model);
            await _adminAccountRepository.SaveChangeAsync();
            model.Id = entity.Id;

            return model;
        }

        public AuthenticationDto CheckAuthentication(string accessToken)
        {
            var descryptToken = string.Empty;
            try
            {
                descryptToken = _protector.Unprotect(accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var model = JsonConvert.DeserializeObject<AuthenticationDto>(descryptToken);
            if (model == null || model.UserId <= 0)
            {
                model = null;
            }
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _adminAccountRepository.Delete(id);
            var recordUpdated = await _adminAccountRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _adminAccountRepository.SaveChangeAsync();
                result = true;
            }
            return result;
        }

        public async Task<AccountDto> GetById(int id)
        {
            var result = await _adminAccountRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            result.PasswordHash = string.Empty;

            return _mapper.Map<AccountDto>(result);
        }

        public async Task<IPaginatedList<AccountDto>> GetAll(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var adminAccountRepo = _adminAccountRepository.Repo;

            var result = await (from adminAccount in adminAccountRepo
                                join adminGroup in _adminGroupRepository.Repo on adminAccount.TypeId equals adminGroup.Id into gs
                                from adminGroup in gs.DefaultIfEmpty()
                                select new AccountDto
                                {
                                    Id = adminAccount.Id,
                                    TypeId = adminAccount.TypeId.GetValueOrDefault(),
                                    TypeIdName = adminGroup.Name,
                                    StaffId = adminAccount.StaffId.GetValueOrDefault(),
                                    UserName = adminAccount.UserName,
                                    Email = adminAccount.Email,
                                    Mobile = adminAccount.Mobile,
                                    FullName = adminAccount.FullName,
                                    Gender = adminAccount.Gender,
                                    BirthDate = adminAccount.BirthDate,
                                    Zip = adminAccount.Zip.GetValueOrDefault(),
                                    Address = adminAccount.Address,
                                    Avatar = adminAccount.Avatar,
                                    Active = adminAccount.Active,
                                    Status = adminAccount.Status,
                                    CreatedDate = adminAccount.CreatedDate
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<LoginResponse> Login(LoginDto model)
        {
            var result = new LoginResponse
            {
                Status = EResponseStatus.Fail
            };

            if (!string.IsNullOrWhiteSpace(model.UserName) && !string.IsNullOrWhiteSpace(model.Password))
            {
                var user = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.UserName.Equals(model.UserName));
                if (user != null && user.Active == (int)EActive.Active && user.Status == (int)EStatus.Using && user.PasswordHash.Equals(model.Password))
                {
                    var authentication = new AuthenticationDto
                    {
                        UserId = user.Id,
                        TypeId = user.TypeId.GetValueOrDefault(),
                        StaffId = user.StaffId.GetValueOrDefault(),
                        Username = user.UserName,
                        CreatedTime = DateTime.Now
                    };
                    var accessToken = _protector.Protect(JsonConvert.SerializeObject(authentication));
                    user.Description = accessToken;
                    await _adminAccountRepository.SaveChangeAsync();
                    result.Status = EResponseStatus.Success;
                    result.Type = user.TypeId.GetValueOrDefault();
                    result.AccessToken = accessToken;
                    result.Displayname = user.FullName != null && user.FullName.Length > 0 ? user.FullName : "admin";
                    result.Email = user.Email != null && user.Email.Length > 0 ? user.Email : "";
                    result.Avatar = user.Avatar != null && user.Avatar.Length > 0 ? user.Avatar : "";
                    result.Permissions = await _adminGroupPermissionRepository.GetPermissionByGroup(user.TypeId.GetValueOrDefault());
                }
            }

            return result;
        }

        public async Task<LoginResponse> LoginWithToken(string token)
        {
            var result = new LoginResponse
            {
                Status = EResponseStatus.Fail
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                var user = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.Description.Equals(token));
                if (user != null && user.Active == (int)EActive.Active && user.Status == (int)EStatus.Using)
                {
                    var authentication = new AuthenticationDto
                    {
                        UserId = user.Id,
                        TypeId = user.TypeId.GetValueOrDefault(),
                        StaffId = user.StaffId.GetValueOrDefault(),
                        Username = user.UserName,
                        CreatedTime = DateTime.Now
                    };
                    var accessToken = _protector.Protect(JsonConvert.SerializeObject(authentication));
                    user.Description = accessToken;
                    await _adminAccountRepository.SaveChangeAsync();
                    result.Status = EResponseStatus.Success;
                    result.Type = user.TypeId.GetValueOrDefault();
                    result.AccessToken = accessToken;
                    result.Displayname = user.FullName != null && user.FullName.Length > 0 ? user.FullName : "admin";
                    result.Email = user.Email != null && user.Email.Length > 0 ? user.Email : "";
                    result.Avatar = user.Avatar != null && user.Avatar.Length > 0 ? user.Avatar : "";
                    result.Permissions = await _adminGroupPermissionRepository.GetPermissionByGroup(user.TypeId.GetValueOrDefault());
                }
            }

            return result;
        }

        public async Task<bool> Update(AccountDto model)
        {
            var result = false;
            var record = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.StaffId = model.StaffId;
                record.TypeId = model.TypeId;
                record.UserName = model.UserName;
                record.Email = model.Email;
                record.Mobile = model.Mobile;
                record.FullName = model.FullName;
                record.Gender = model.Gender;
                if (!string.IsNullOrEmpty(model.PasswordHash))
                {
                    record.PasswordHash = model.PasswordHash;
                }
                record.BirthDate = model.BirthDate;
                record.StateId = model.StateId;
                record.CityId = model.CityId;
                record.Zip = model.Zip;
                record.Address = model.Address;
                record.Avatar = model.Avatar;
                record.CreatedDate = model.CreatedDate;
                record.UpdatedDate = model.UpdatedDate;
                record.UpdatedStaffId = model.UpdatedStaffId;

                await _adminAccountRepository.SaveChangeAsync();

                result = true;
            }
            return result;
        }
        public async Task<bool> CheckUserNameExist(string username)
        {
            var result = true;
            var record = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.UserName == username);

            if (record != null)
            {
                result = false;
            }
            return result;
        }
        public async Task<bool> CheckEmailExist(string email)
        {
            var result = true;
            var record = await _adminAccountRepository.Repo.FirstOrDefaultAsync(c => c.Email == email);

            if (record != null)
            {
                result = false;
            }
            return result;
        }
    }
}
