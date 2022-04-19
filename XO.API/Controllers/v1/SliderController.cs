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
    public class SliderController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly ISliderBusiness _sliderBusiness;
        public SliderController(IHttpContextAccessor httpContextAccessor,
            ISliderBusiness sliderBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _sliderBusiness = sliderBusiness;
        }
        // GET: /Slider
        [ClaimRequirement("", "slider_list")]
        [HttpGet]
        public Task<IPaginatedList<Slider>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _sliderBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /Slider/5
        [ClaimRequirement("", "slider_update")]
        [HttpGet("{id}")]
        public Task<Slider> Get(int id)
        {
            return _sliderBusiness.GetById(id);
        }
        // POST: /Slider
        [ClaimRequirement("", "slider_create")]
        [HttpPost]
        public async Task<int> Post(Slider model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _sliderBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Slider/5
        [ClaimRequirement("", "slider_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Slider model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _sliderBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Slider/active
        [HttpPut("active")]
        [ClaimRequirement("", "slider_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _sliderBusiness.SetActive(id, Status);
        }
    }
}