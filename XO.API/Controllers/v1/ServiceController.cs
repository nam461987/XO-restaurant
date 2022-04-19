using System.Collections.Generic;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Dtos.Service;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IServiceBusiness _serviceBusiness;
        public ServiceController(IHttpContextAccessor httpContextAccessor,
            IServiceBusiness serviceBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _serviceBusiness = serviceBusiness;
        }
        // GET: /Service
        [ClaimRequirement("", "category_service_list")]
        [HttpGet]
        public Task<IPaginatedList<ServiceDto>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _serviceBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        [ClaimRequirement("", "category_service_list")]
        [HttpGet("getbytype/{type}")]
        public Task<List<ServiceDto>> GetByType(int type)
        {
            return _serviceBusiness.GetByType(_authenticationDto.TypeId, type);
        }
        // GET: /Service/5
        [ClaimRequirement("", "category_service_update")]
        [HttpGet("{id}")]
        public Task<Service> Get(int id)
        {
            return _serviceBusiness.GetById(id);
        }
        // POST: /Service
        [ClaimRequirement("", "category_service_create")]
        [HttpPost]
        public async Task<int> Post(Service model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _serviceBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Service/5
        [ClaimRequirement("", "category_service_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Service model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _serviceBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Service/active
        [HttpPut("active")]
        [ClaimRequirement("", "category_service_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _serviceBusiness.SetActive(id, Status);
        }
    }
}