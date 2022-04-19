using XO.Business.Interfaces.Paginated;
using XO.Common.Dtos.AdminAccount;
using XO.Common.Responses.AdminAccount;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces.Admin
{
    public interface IAdminAccountBusiness
    {
        Task<AdminAccount> Add(AdminAccount model);
        Task<bool> Update(AccountDto model);
        Task<bool> Delete(int id);
        Task<bool> SetActive(int id, int Active);
        Task<IPaginatedList<AccountDto>> GetAll(int pageIndex, int pageSize);
        Task<AccountDto> GetById(int id);

        Task<LoginResponse> Login(LoginDto model);
        Task<LoginResponse> LoginWithToken(string token);
        AuthenticationDto CheckAuthentication(string accessToken);
    }
}
