using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XO.API.Extensions;
using XO.Business.Interfaces.Admin;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Responses.AdminAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XO.API.Controllers.v1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAdminAccountBusiness _adminAccountBusiness;

        public LoginController(ILogger<LoginController> logger,
            IAdminAccountBusiness adminAccountBusiness)
        {
            _logger = logger;
            _adminAccountBusiness = adminAccountBusiness;
        }

        [HttpPost]
        public async Task<LoginResponse> Login(LoginDto model)
        {
            model.Password = WebsiteExtension.EncryptPassword(model.Password);
            return await _adminAccountBusiness.Login(model);
        }

        [HttpGet]
        public async Task<LoginResponse> LoginWithToken(string token)
        {
            return await _adminAccountBusiness.LoginWithToken(token);
        }
    }
}