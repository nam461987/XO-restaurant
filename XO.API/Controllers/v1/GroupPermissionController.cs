using System.Collections.Generic;
using System.Threading.Tasks;
using XO.API.Attributes;
using XO.API.Extensions;
using XO.Business.Interfaces.Admin;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Models;
using XO.Entities.ModelExtensions;
using XO.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XO.API.Controllers.v1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class GroupPermissionController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IAdminGroupPermissionBusiness _groupPermissionBusiness;
        public GroupPermissionController(IHttpContextAccessor httpContextAccessor,
            IAdminGroupPermissionBusiness groupPermissionBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _groupPermissionBusiness = groupPermissionBusiness;
        }

        // GET: /grouppermission/getpermission
        [ClaimRequirement("", "admin_group_permission_list")]
        //[Route("getpermission")]
        [HttpGet("getpermission")]
        public async Task<List<AdminGroupPermission_View00>> Get(int groupId, string module)
        {
            //Because we just get user permissions by restaurant,
            //we set BranchId = 0.
            //If we want to get by each Branch, just put current user branchId
            return await _groupPermissionBusiness.GetPermissionByGroupAndModule(groupId, module);
        }
        // GET: /grouppermission/getgroup
        //[Route("getgroup")]
        [HttpGet("getgroup")]
        public async Task<List<AdminGroup>> GetGroup()
        {
            return await _groupPermissionBusiness.GetGroup(_authenticationDto.TypeId);
        }
        //[Route("getmodule")]
        [HttpGet("getmodule")]
        public async Task<List<Option2Model>> GetModule()
        {
            return await _groupPermissionBusiness.GetModule();
        }
        // PUT: /grouppermission
        [ClaimRequirement("", "admin_group_permission_create,admin_group_permission_update")]
        [HttpPut]
        public async Task<int> Put(int groupId, int permissionId, int status)
        {
            return await _groupPermissionBusiness.InsertOrUpdatePermission(groupId, permissionId, status);
        }
    }
}