using XO.Business.Interfaces;
using XO.Common.Enums;
using XO.Common.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Repository.Interfaces.Admin;

namespace XO.Business
{
    public class OptionBusiness : IOptionBusiness
    {
        private readonly IMapper _mapper;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBlogTypeRepository _blogTypeRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IAdminGroupRepository _adminGroupRepository;
        public OptionBusiness(IMapper mapper,
            IServiceTypeRepository serviceTypeRepository,
            IServiceRepository serviceRepository,
            IBlogTypeRepository blogTypeRepository,
            IBranchRepository branchRepository,
            IStaffRepository staffRepository,
            IAdminGroupRepository adminGroupRepository)
        {
            _mapper = mapper;
            _serviceTypeRepository = serviceTypeRepository;
            _serviceRepository = serviceRepository;
            _blogTypeRepository = blogTypeRepository;
            _branchRepository = branchRepository;
            _staffRepository = staffRepository;
            _adminGroupRepository = adminGroupRepository;
        }
        public async Task<List<OptionModel>> GetServiceTypeOptions()
        {
            var options = new List<OptionModel>();

            var item = await _serviceTypeRepository.Repo.Where(c => c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }
        public async Task<List<OptionModel>> GetSubServiceOptions()
        {
            var options = new List<OptionModel>();

            var item = await _serviceRepository.Repo.Where(c => (c.SubServiceId == null ||
            c.SubServiceId == 0) && c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }
        public async Task<List<OptionModel>> GetBlogTypeOptions()
        {
            var options = new List<OptionModel>();

            var item = await _blogTypeRepository.Repo.Where(c => c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }
        public async Task<List<OptionModel>> GetBranchOptions()
        {
            var options = new List<OptionModel>();

            var item = await _branchRepository.Repo.Where(c => c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }
        public async Task<List<OptionModel>> GetStaffOptions()
        {
            var options = new List<OptionModel>();

            var item = await _staffRepository.Repo.Where(c => c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }

        public async Task<List<OptionModel>> GetGroupOptions()
        {
            var options = new List<OptionModel>();

            var item = await _adminGroupRepository.Repo.Where(c => c.Status == (int)EStatus.Using)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (item != null)
            {
                options.AddRange(item.Select(c => new OptionModel
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return options;
        }
    }
}
