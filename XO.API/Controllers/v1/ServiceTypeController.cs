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
    public class ServiceTypeController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IServiceTypeBusiness _serviceTypeBusiness;
        public ServiceTypeController(IHttpContextAccessor httpContextAccessor,
            IServiceTypeBusiness serviceTypeBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _serviceTypeBusiness = serviceTypeBusiness;
        }
        // GET: /ServiceType
        [ClaimRequirement("", "category_service_type_list")]
        [HttpGet]
        public Task<IPaginatedList<ServiceType>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _serviceTypeBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /ServiceType/5
        [ClaimRequirement("", "category_service_type_update")]
        [HttpGet("{id}")]
        public Task<ServiceType> Get(int id)
        {
            return _serviceTypeBusiness.GetById(id);
        }
        // POST: /ServiceType
        [ClaimRequirement("", "category_service_type_create")]
        [HttpPost]
        public async Task<int> Post(ServiceType model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _serviceTypeBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /ServiceType/5
        [ClaimRequirement("", "category_service_type_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(ServiceType model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _serviceTypeBusiness.Update(model);
            }
            return result;
        }
        // PUT: /ServiceType/active
        [HttpPut("active")]
        [ClaimRequirement("", "category_service_type_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _serviceTypeBusiness.SetActive(id, Status);
        }
    }
}