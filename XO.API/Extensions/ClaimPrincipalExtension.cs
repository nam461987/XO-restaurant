using XO.Common.Dtos.AdminAccount;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace XO.API.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static AuthenticationDto ToAuthenticationDto(this ClaimsPrincipal source)
           => JsonConvert.DeserializeObject<AuthenticationDto>(source
               .Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value);
    }
}
