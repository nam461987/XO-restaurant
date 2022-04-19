using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Dtos.Staff;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IStaffBusiness _staffBusiness;
        public StaffController(IHttpContextAccessor httpContextAccessor,
            IStaffBusiness staffBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _staffBusiness = staffBusiness;
        }
        // GET: /Staff
        [ClaimRequirement("", "staff_list")]
        [HttpGet]
        public Task<IPaginatedList<StaffDto>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _staffBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /Staff/5
        [ClaimRequirement("", "staff_update")]
        [HttpGet("{id}")]
        public Task<Staff> Get(int id)
        {
            return _staffBusiness.GetById(id);
        }
        // POST: /Staff
        [ClaimRequirement("", "staff_create")]
        [HttpPost]
        public async Task<int> Post(Staff model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _staffBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Staff/5
        [ClaimRequirement("", "staff_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Staff model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _staffBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Staff/active
        [HttpPut("active")]
        [ClaimRequirement("", "staff_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _staffBusiness.SetActive(id, Status);
        }
    }
}