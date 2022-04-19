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
    public class BlogTypeController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IBlogTypeBusiness _blogTypeBusiness;
        public BlogTypeController(IHttpContextAccessor httpContextAccessor,
            IBlogTypeBusiness blogTypeBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _blogTypeBusiness = blogTypeBusiness;
        }
        // GET: /BlogType
        [ClaimRequirement("", "blog_type_list")]
        [HttpGet]
        public Task<IPaginatedList<BlogType>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _blogTypeBusiness.GetAll(pageIndex, pageSize);
        }
        // GET: /BlogType/5
        [ClaimRequirement("", "blog_type_update")]
        [HttpGet("{id}")]
        public Task<BlogType> Get(int id)
        {
            return _blogTypeBusiness.GetById(id);
        }
        // POST: /BlogType
        [ClaimRequirement("", "blog_type_create")]
        [HttpPost]
        public async Task<int> Post(BlogType model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _blogTypeBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /BlogType/5
        [ClaimRequirement("", "blog_type_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(BlogType model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _blogTypeBusiness.Update(model);
            }
            return result;
        }
        // PUT: /BlogType/active
        [HttpPut("active")]
        [ClaimRequirement("", "blog_type_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _blogTypeBusiness.SetActive(id, Status);
        }
    }
}