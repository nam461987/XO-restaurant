using XO.API.Extensions;
using XO.Repository.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace XO.API.Filters
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        private readonly IAdminGroupPermissionRepository _adminGroupPermissionRepository;

        public ClaimRequirementFilter(Claim claim,
            IAdminGroupPermissionRepository adminGroupPermissionRepository)
        {
            _claim = claim;
            _adminGroupPermissionRepository = adminGroupPermissionRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _authenticationDto = context.HttpContext.User.ToAuthenticationDto();
            var Permissions = _adminGroupPermissionRepository.GetPermissionByGroup(_authenticationDto.TypeId);
            string[] roles = _claim.Value.Split(',');
            bool hasClaim = false;

            foreach (var r in roles)
            {
                if (Permissions.Result.Contains(r))
                {
                    hasClaim = true;
                }
            }
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
