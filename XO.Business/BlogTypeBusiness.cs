using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using XO.Business.Filter;
using System.Collections.Generic;

namespace XO.Business
{
    public class BlogTypeBusiness : IBlogTypeBusiness
    {
        private readonly IBlogTypeRepository _blogTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogTypeBusiness(IMapper mapper,
            IBlogTypeRepository blogTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _blogTypeRepository = blogTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogType> Add(BlogType model)
        {
            var entity = _blogTypeRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _blogTypeRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _blogTypeRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<BlogType>> GetAll(int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var result = await _blogTypeRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .OrderBy(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IEnumerable<BlogType>> GetAllWithoutPaging(int accountTypeId)
        {
            var result = await _blogTypeRepository.Repo.Where(c => c.Status < (int)EStatus.All)
                .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(accountTypeId)
                          .ToListAsync();
            return result;
        }

        public async Task<BlogType> GetById(int id)
        {
            var result = await _blogTypeRepository.Repo.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(BlogType model)
        {
            var result = false;
            var record = await _blogTypeRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.Name = model.Name;
                record.Description = model.Description;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
