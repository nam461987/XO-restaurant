using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Common.Dtos.AdminAccount;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly ICompanyBusiness _companyBusiness;
        public CompanyController(IHttpContextAccessor httpContextAccessor,
            ICompanyBusiness companyBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _companyBusiness = companyBusiness;
        }
        // GET: /Company/5
        [ClaimRequirement("", "company_update")]
        [HttpGet]
        public Task<Company> Get()
        {
            return _companyBusiness.GetInformation();
        }
        // PUT: /Blog/5
        [ClaimRequirement("", "company_update")]
        [HttpPut]
        public async Task<bool> Put(Company model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _companyBusiness.Update(model);
            }
            return result;
        }
    }
}