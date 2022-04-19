using System;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Dtos.Blog;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IBlogBusiness _blogBusiness;
        public BlogController(IHttpContextAccessor httpContextAccessor,
            IBlogBusiness blogBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _blogBusiness = blogBusiness;
        }
        // GET: /Blog
        [ClaimRequirement("", "blog_list")]
        [HttpGet]
        public Task<IPaginatedList<BlogDto>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _blogBusiness.GetAll(_authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /Blog
        [ClaimRequirement("", "blog_list")]
        [HttpGet("getbytype/{type}")]
        public Task<IPaginatedList<BlogDto>> GetByTypeId(int type, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _blogBusiness.GetByTypeId(type, _authenticationDto.TypeId, pageIndex, pageSize);
        }
        // GET: /Blog/5
        [ClaimRequirement("", "blog_update")]
        [HttpGet("{id}")]
        public Task<BlogDto> Get(int id)
        {
            return _blogBusiness.GetById(id);
        }
        // POST: /Blog
        [ClaimRequirement("", "blog_create")]
        [HttpPost]
        public async Task<int> Post(Blog model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                model.CreatedDate = DateTime.Now;
                model.CreatedStaffId = _authenticationDto.UserId;
                var modelInsert = await _blogBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Blog/5
        [ClaimRequirement("", "blog_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Blog model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                model.UpdatedDate = DateTime.Now;
                model.UpdatedStaffId = _authenticationDto.UserId;
                result = await _blogBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Blog/active
        [HttpPut("active")]
        [ClaimRequirement("", "blog_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _blogBusiness.SetActive(id, Status);
        }
    }
}