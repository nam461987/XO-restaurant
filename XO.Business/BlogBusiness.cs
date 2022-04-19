using XO.Business.Filter;
using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Dtos.Blog;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XO.Business
{
    public class BlogBusiness : IBlogBusiness
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogBusiness(IMapper mapper,
            IBlogRepository blogRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Blog> Add(Blog model)
        {
            var entity = _blogRepository.Add(model);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _blogRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _blogRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<BlogDto>> GetAll(int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var BlogRepo = _blogRepository.Repo;

            var result = await (from blog in BlogRepo
                                select new BlogDto
                                {
                                    Id = blog.Id,
                                    TypeId = blog.TypeId,
                                    TypeIdName = blog.Type.Name,
                                    TypeIdStatus = blog.Type.Status,
                                    Name = blog.Name,
                                    Description = blog.Description,
                                    NewsContent = blog.NewsContent,
                                    PostedDate = blog.PostedDate,
                                    Tags = blog.Tags,
                                    Image = blog.Image,
                                    HotNews = blog.HotNews,
                                    CreatedDate = blog.CreatedDate,
                                    CreatedStaffId = blog.CreatedStaffId,
                                    CreatedStaffIdName = blog.CreatedStaff.UserName,
                                    UpdatedDate = blog.UpdatedDate,
                                    UpdatedStaffId = blog.UpdatedStaffId,
                                    UpdatedStaffIdName = blog.UpdatedStaff.UserName,
                                    Status = blog.Status.GetValueOrDefault()
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.TypeIdStatus, f => f.Status, accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<IPaginatedList<BlogDto>> GetByTypeId(int typeId, int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var BlogRepo = _blogRepository.Repo;

            var result = await (from blog in BlogRepo
                                select new BlogDto
                                {
                                    Id = blog.Id,
                                    TypeId = blog.TypeId,
                                    TypeIdName = blog.Type.Name,
                                    TypeIdStatus = blog.Type.Status,
                                    Name = blog.Name,
                                    Description = blog.Description,
                                    NewsContent = blog.NewsContent,
                                    PostedDate = blog.PostedDate,
                                    Tags = blog.Tags,
                                    Image = blog.Image,
                                    HotNews = blog.HotNews,
                                    CreatedDate = blog.CreatedDate,
                                    CreatedStaffId = blog.CreatedStaffId,
                                    CreatedStaffIdName = blog.CreatedStaff.UserName,
                                    UpdatedDate = blog.UpdatedDate,
                                    UpdatedStaffId = blog.UpdatedStaffId,
                                    UpdatedStaffIdName = blog.UpdatedStaff.UserName,
                                    Status = blog.Status.GetValueOrDefault()
                                })
                          .Where(c => c.TypeId == typeId && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.TypeIdStatus, f => f.Status, accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }
        public async Task<IEnumerable<BlogDto>> GetHotBlogs(int accountTypeId)
        {
            var BlogRepo = _blogRepository.Repo;

            var result = await (from blog in BlogRepo
                                select new BlogDto
                                {
                                    Id = blog.Id,
                                    TypeId = blog.TypeId,
                                    TypeIdName = blog.Type.Name,
                                    TypeIdStatus = blog.Type.Status,
                                    Name = blog.Name,
                                    Description = blog.Description,
                                    NewsContent = blog.NewsContent,
                                    PostedDate = blog.PostedDate,
                                    Tags = blog.Tags,
                                    Image = blog.Image,
                                    HotNews = blog.HotNews,
                                    CreatedDate = blog.CreatedDate,
                                    CreatedStaffId = blog.CreatedStaffId,
                                    CreatedStaffIdName = blog.CreatedStaff.UserName,
                                    UpdatedDate = blog.UpdatedDate,
                                    UpdatedStaffId = blog.UpdatedStaffId,
                                    UpdatedStaffIdName = blog.UpdatedStaff.UserName,
                                    Status = blog.Status.GetValueOrDefault()
                                })
                          .Where(c => c.HotNews == 1 && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.TypeIdStatus, f => f.Status, accountTypeId)
                          .OrderByDescending(c => c.PostedDate)
                          .Take(4)
                          .ToListAsync();

            return result;
        }
        public async Task<BlogDto> GetById(int id)
        {
            var BlogRepo = _blogRepository.Repo;

            var result = await (from blog in BlogRepo
                                select new BlogDto
                                {
                                    Id = blog.Id,
                                    TypeId = blog.TypeId,
                                    TypeIdName = blog.Type.Name,
                                    TypeIdStatus = blog.Type.Status,
                                    Name = blog.Name,
                                    Description = blog.Description,
                                    NewsContent = blog.NewsContent,
                                    PostedDate = blog.PostedDate,
                                    Tags = blog.Tags,
                                    Image = blog.Image,
                                    HotNews = blog.HotNews,
                                    CreatedDate = blog.CreatedDate,
                                    CreatedStaffId = blog.CreatedStaffId,
                                    CreatedStaffIdName = blog.CreatedStaff.UserName,
                                    UpdatedDate = blog.UpdatedDate,
                                    UpdatedStaffId = blog.UpdatedStaffId,
                                    UpdatedStaffIdName = blog.UpdatedStaff.UserName,
                                    Status = blog.Status.GetValueOrDefault()
                                })
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Update(Blog model)
        {
            var result = false;
            var record = await _blogRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.TypeId = model.TypeId;
                record.Name = model.Name;
                record.Description = model.Description;
                record.NewsContent = model.NewsContent;
                record.PostedDate = model.PostedDate;
                record.Tags = model.Tags;
                record.Image = model.Image;
                record.HotNews = model.HotNews;
                record.UpdatedDate = model.UpdatedDate;
                record.UpdatedStaffId = model.UpdatedStaffId;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
        public async Task<IEnumerable<Blog>> GetPreAndNextBlog(int blogId)
        {
            var BlogRepo = _blogRepository.Repo;

            var preBlog = await BlogRepo
                .Where(c => c.Status == (int)EStatus.Using && c.Id < blogId)
                .Take(1)
                .ToListAsync();

            var nextBlog = await BlogRepo
                .Where(c => c.Status == (int)EStatus.Using && c.Id > blogId)
                .Take(1)
                .ToListAsync();

            var obj = new List<Blog>();

            if (preBlog.Any())
            {
                obj.Add(new Blog { Id = preBlog.FirstOrDefault().Id, Name = preBlog.FirstOrDefault().Name });
            }
            else
            {
                obj.Add(new Blog { Id = 0, Name = "" });
            }

            if (nextBlog.Any())
            {
                obj.Add(new Blog { Id = nextBlog.FirstOrDefault().Id, Name = nextBlog.FirstOrDefault().Name });
            }
            else
            {
                obj.Add(new Blog { Id = 0, Name = "" });
            }

            return obj;
        }
        public async Task<IEnumerable<BlogDto>> GetTwoRandomItems(int accountTypeId)
        {
            var BlogRepo = _blogRepository.Repo;

            var result = await (from blog in BlogRepo
                                select new BlogDto
                                {
                                    Id = blog.Id,
                                    TypeId = blog.TypeId,
                                    TypeIdName = blog.Type.Name,
                                    TypeIdStatus = blog.Type.Status,
                                    Name = blog.Name,
                                    Description = blog.Description,
                                    NewsContent = blog.NewsContent,
                                    PostedDate = blog.PostedDate,
                                    Tags = blog.Tags,
                                    Image = blog.Image,
                                    HotNews = blog.HotNews,
                                    CreatedDate = blog.CreatedDate,
                                    CreatedStaffId = blog.CreatedStaffId,
                                    CreatedStaffIdName = blog.CreatedStaff.UserName,
                                    UpdatedDate = blog.UpdatedDate,
                                    UpdatedStaffId = blog.UpdatedStaffId,
                                    UpdatedStaffIdName = blog.UpdatedStaff.UserName,
                                    Status = blog.Status.GetValueOrDefault()
                                })
                          .Where(c => c.Status == (int)EStatus.Using)
                          .OrderBy(r => Guid.NewGuid()).Take(2)
                          .ToListAsync();

            return result;
        }
    }
}
