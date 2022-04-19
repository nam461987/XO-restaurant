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
    public class StaffPortfolioController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IStaffPortfolioBusiness _staffPortfolioBusiness;
        private readonly int _staffId;
        public StaffPortfolioController(IHttpContextAccessor httpContextAccessor,
            IStaffPortfolioBusiness staffPortfolioBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _staffPortfolioBusiness = staffPortfolioBusiness;
            _staffId = _authenticationDto.StaffId;
        }
        // GET: /StaffPortfolio
        [ClaimRequirement("", "portfolio_list")]
        [HttpGet]
        public async Task<IPaginatedList<StaffPortfolio>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return await _staffPortfolioBusiness.GetAll(_authenticationDto.TypeId,
                _staffId, pageIndex, pageSize);
        }
        // GET: /StaffPortfolio/5
        [ClaimRequirement("", "portfolio_update")]
        [HttpGet("{id}")]
        public Task<StaffPortfolio> Get(int id)
        {
            return _staffPortfolioBusiness.GetById(id);
        }
        // POST: /StaffPortfolio
        [ClaimRequirement("", "portfolio_create")]
        [HttpPost]
        public async Task<int> Post(StaffPortfolio model)
        {
            var result = 0;
            // modify StaffId
            //if admin group add, no need to modify
            //modify if current staff add
            model.StaffId = model.StaffId != 0 ? model.StaffId : _staffId;

            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _staffPortfolioBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /StaffPortfolio/5
        [ClaimRequirement("", "portfolio_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(StaffPortfolio model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _staffPortfolioBusiness.Update(model);
            }
            return result;
        }
        // PUT: /StaffPortfolio/active
        [HttpPut("active")]
        [ClaimRequirement("", "portfolio_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _staffPortfolioBusiness.SetActive(id, Status);
        }
    }
}