using XO.Business.Interfaces;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace XO.Business
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyBusiness(IMapper mapper, 
            ICompanyRepository companyRepository,
            IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Company> GetInformation()
        {
            var result = await _companyRepository.Repo
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(Company model)
        {
            var result = false;
            var record = await _companyRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Name = model.Name;
                record.Address = model.Address;
                record.Fax = model.Fax;
                record.Phone = model.Phone;
                record.OpenTime = model.OpenTime;
                record.Email = model.Email;
                record.Facebook = model.Facebook;
                record.Youtube = model.Youtube;
                record.Instagram = model.Instagram;
                record.Twitter = model.Twitter;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
