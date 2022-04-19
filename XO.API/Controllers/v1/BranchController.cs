using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IBranchBusiness _branchBusiness;
        public BranchController(IHttpContextAccessor httpContextAccessor,
            IBranchBusiness branchBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _branchBusiness = branchBusiness;
        }
        // GET: /Branch
        [ClaimRequirement("", "category_branch_list")]
        [HttpGet]
        public Task<IPaginatedList<Branch>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _branchBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /Branch/5
        [ClaimRequirement("", "category_branch_update")]
        [HttpGet("{id}")]
        public Task<Branch> Get(int id)
        {
            return _branchBusiness.GetById(id);
        }
        // POST: /Branch
        [ClaimRequirement("", "category_branch_create")]
        [HttpPost]
        public async Task<int> Post(Branch model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _branchBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Branch/5
        [ClaimRequirement("", "category_branch_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Branch model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _branchBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Branch/active
        [HttpPut("active")]
        [ClaimRequirement("", "category_branch_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _branchBusiness.SetActive(id, Status);
        }
    }
}