using XO.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IOptionBusiness
    {
        Task<List<OptionModel>> GetServiceTypeOptions();
        Task<List<OptionModel>> GetSubServiceOptions(); 
        Task<List<OptionModel>> GetBlogTypeOptions();
        Task<List<OptionModel>> GetBranchOptions();
        Task<List<OptionModel>> GetStaffOptions();
        Task<List<OptionModel>> GetGroupOptions();
    }
}
