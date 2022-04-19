using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IOptionBusiness _optionBusiness;
        public OptionController(IHttpContextAccessor httpContextAccessor,
            IOptionBusiness optionBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _optionBusiness = optionBusiness;
        }
        //[Route("getservicetype")]
        [HttpGet("getservicetype")]
        public async Task<List<OptionModel>> GetServiceTypeOptions()
        {
            return await _optionBusiness.GetServiceTypeOptions();
        }
        //[Route("getsubservice")]
        [HttpGet("getsubservice")]
        public async Task<List<OptionModel>> GetSubServiceOptions()
        {
            return await _optionBusiness.GetSubServiceOptions();
        }
        //[Route("getblogtype")]
        [HttpGet("getblogtype")]
        public async Task<List<OptionModel>> GetBlogTypeOptions()
        {
            return await _optionBusiness.GetBlogTypeOptions();
        }
        //[Route("getbranch")]
        [HttpGet("getbranch")]
        public async Task<List<OptionModel>> GetBranchOptions()
        {
            return await _optionBusiness.GetBranchOptions();
        }
        //[Route("getstaff")]
        [HttpGet("getstaff")]
        public async Task<List<OptionModel>> GetStaffOptions()
        {
            return await _optionBusiness.GetStaffOptions();
        }
        //[Route("getgroup")]
        [HttpGet("getgroup")]
        public async Task<List<OptionModel>> GetGroupOptions()
        {
            return await _optionBusiness.GetGroupOptions();
        }
    }
}