using XO.Common.Dtos.AdminAccount;
using XO.Entities.Models;
using AutoMapper;

namespace XO.API.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AdminAccount, AccountDto>();
            CreateMap<AccountDto, AdminAccount>();
        }
    }
}
