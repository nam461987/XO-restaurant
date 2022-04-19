using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces;
using XO.Common.Dtos.AdminAccount;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace XO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IUploadBusiness _uploadBusiness;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _appSetting;
        public UploadController(IHttpContextAccessor httpContextAccessor,
            IUploadBusiness uploadBusiness,
            IHostingEnvironment hostingEnvironment,
            IConfiguration appSetting)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _uploadBusiness = uploadBusiness;
            _hostingEnvironment = hostingEnvironment;
            _appSetting = appSetting;
        }
        [HttpPost]
        public Task<string> MultipleUpload(IFormFile file)
        {
            string forwardFolder = _appSetting.GetValue<string>("AppSettings:ForwardUploadFolderRoot");
            string uploadFolder = _appSetting.GetValue<string>("AppSettings:UploadFolderRoot");

            return _uploadBusiness.MultipleUpload(file, forwardFolder, uploadFolder);
        }
    }
}