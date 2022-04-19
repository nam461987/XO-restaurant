using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces.Admin;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XO.Entities.Models;

namespace XO.API.Controllers.v1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class AdminAccountController : ControllerBase
    {
        private readonly ILogger<AdminAccountController> _logger;
        private readonly IAdminAccountBusiness _adminAccountBusiness;
        private readonly IConfiguration _appSetting;
        private readonly IMapper _mapper;

        public AdminAccountController(
            IAdminAccountBusiness adminAccountBusiness)
        {
            _adminAccountBusiness = adminAccountBusiness;
        }

        // GET: /adminaccount
        [ClaimRequirement("", "admin_user_list")]
        [HttpGet]
        public async Task<IPaginatedList<AccountDto>> Get(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return await _adminAccountBusiness.GetAll(pageIndex, pageSize);
        }
        // GET: /adminaccount/5
        [ClaimRequirement("", "admin_user_update")]
        [HttpGet("{id}")]
        public async Task<AccountDto> Get(int id)
        {
            return await _adminAccountBusiness.GetById(id);
        }
        // POST: /admingroup
        [ClaimRequirement("", "admin_user_create")]
        [HttpPost]
        public async Task<int> Post(AdminAccount model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                model.PasswordHash = WebsiteExtension.EncryptPassword(model.PasswordHash);

                var modelInsert = await _adminAccountBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /admingroup/5
        [ClaimRequirement("", "admin_user_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(AccountDto model)
        {
            var result = false;

            model.PasswordHash = WebsiteExtension.EncryptPassword(model.PasswordHash);

            if (ModelState.IsValid)
            {
                result = await _adminAccountBusiness.Update(model);
            }
            return result;
        }
    }
}