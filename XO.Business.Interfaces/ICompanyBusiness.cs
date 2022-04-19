using XO.Business.Interfaces.Paginated;
using XO.Entities.Models;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface ICompanyBusiness
    {
        Task<bool> Update(Company model);
        Task<Company> GetInformation();
    }
}
