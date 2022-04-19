using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Dtos.Photo;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IPhotoBusiness _photoBusiness;
        public PhotoController(IHttpContextAccessor httpContextAccessor,
            IPhotoBusiness photoBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _photoBusiness = photoBusiness;
        }
        // GET: /Photo
        [ClaimRequirement("", "photo_list")]
        [HttpGet]
        public Task<IPaginatedList<PhotoDto>> Get(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _photoBusiness.GetAll(_authenticationDto.TypeId,pageIndex, pageSize);
        }
        [ClaimRequirement("", "photo_list")]
        [HttpGet("getallwithoutpaging")]
        public Task<List<PhotoDto>> GetAllWithoutPaging()
        {
            return _photoBusiness.GetAllWithoutPaging(_authenticationDto.TypeId);
        }
        // GET: /Photo/5
        [ClaimRequirement("", "photo_update")]
        [HttpGet("{id}")]
        public Task<Photo> Get(int id)
        {
            return _photoBusiness.GetById(id);
        }
        // POST: /Photo
        [ClaimRequirement("", "photo_create")]
        [HttpPost]
        public async Task<int> Post(Photo model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                model.Status = 1;
                var modelInsert = await _photoBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }
        // PUT: /Photo/5
        [ClaimRequirement("", "photo_update")]
        [HttpPut("{id}")]
        public async Task<bool> Put(Photo model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _photoBusiness.Update(model);
            }
            return result;
        }
        // PUT: /Photo/active
        [HttpPut("active")]
        [ClaimRequirement("", "photo_delete")]
        public Task<bool> Put(int id, int Status)
        {
            return _photoBusiness.SetActive(id, Status);
        }
    }
}