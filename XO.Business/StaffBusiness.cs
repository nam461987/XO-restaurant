using XO.Business.Interfaces;
using XO.Business.Interfaces.Paginated;
using XO.Business.Paginated;
using XO.Common.Constants;
using XO.Common.Enums;
using XO.Entities.Models;
using XO.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using XO.Common.Dtos.Staff;
using XO.Business.Filter;
using System.Collections.Generic;

namespace XO.Business
{
    public class StaffBusiness : IStaffBusiness
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StaffBusiness(IMapper mapper,
            IStaffRepository staffRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Staff> Add(Staff model)
        {
            var entity = _staffRepository.Add(model);
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
            _staffRepository.Delete(id);
            var recordUpdated = await _unitOfWork.SaveChangesAsync();
            return recordUpdated > 0;
        }

        public async Task<bool> SetActive(int id, int Active)
        {
            var result = false;
            var record = await _staffRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (record != null)
            {
                record.Status = Active == 1 ? 0 : 1;
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IPaginatedList<StaffDto>> GetAll(int accountTypeId, int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var StaffRepo = _staffRepository.Repo;

            var result = await (from staff in StaffRepo
                                select new StaffDto
                                {
                                    Id = staff.Id,
                                    BranchId = staff.BranchId,
                                    BranchIdName = staff.Branch.Name,
                                    BranchIdStatus = staff.Branch.Status,
                                    PositionId = staff.PositionId,
                                    PositionIdName = staff.Position.Name,
                                    Name = staff.Name,
                                    BirthDate = staff.BirthDate,
                                    Email = staff.Email,
                                    Mobile = staff.Mobile,
                                    Home = staff.Home,
                                    Facebook = staff.Facebook,
                                    Instagram = staff.Instagram,
                                    Twitter = staff.Twitter,
                                    LinkedIn = staff.LinkedIn,
                                    BookingUrl = staff.BookingUrl,
                                    Address = staff.Address,
                                    StateId = staff.StateId,
                                    CityId = staff.CityId,
                                    Zip = staff.Zip,
                                    Avatar = staff.Avatar,
                                    Gender = staff.Gender,
                                    IdentityNumber = staff.IdentityNumber,
                                    Description = staff.Description,
                                    Status = staff.Status,
                                    CreatedStaffId = staff.CreatedStaffId,
                                    CreatedDate = staff.CreatedDate,
                                    UpdatedStaffId = staff.UpdatedStaffId,
                                    UpdatedDate = staff.UpdatedDate
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.BranchIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<IEnumerable<StaffDto>> GetAllWithoutPaging(int accountTypeId)
        {
            var StaffRepo = _staffRepository.Repo;

            var result = await (from staff in StaffRepo
                                select new StaffDto
                                {
                                    Id = staff.Id,
                                    BranchId = staff.BranchId,
                                    BranchIdName = staff.Branch.Name,
                                    BranchIdStatus = staff.Branch.Status,
                                    PositionId = staff.PositionId,
                                    PositionIdName = staff.Position.Name,
                                    Name = staff.Name,
                                    BirthDate = staff.BirthDate,
                                    Email = staff.Email,
                                    Mobile = staff.Mobile,
                                    Home = staff.Home,
                                    Facebook = staff.Facebook,
                                    Instagram = staff.Instagram,
                                    Twitter = staff.Twitter,
                                    LinkedIn = staff.LinkedIn,
                                    BookingUrl = staff.BookingUrl,
                                    Address = staff.Address,
                                    StateId = staff.StateId,
                                    CityId = staff.CityId,
                                    Zip = staff.Zip,
                                    Avatar = staff.Avatar,
                                    Gender = staff.Gender,
                                    IdentityNumber = staff.IdentityNumber,
                                    Description = staff.Description,
                                    Status = staff.Status,
                                    CreatedStaffId = staff.CreatedStaffId,
                                    CreatedDate = staff.CreatedDate,
                                    UpdatedStaffId = staff.UpdatedStaffId,
                                    UpdatedDate = staff.UpdatedDate
                                })
                          .Where(c => c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.BranchIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .ToListAsync();

            return result;
        }

        public async Task<StaffDto> GetAdminStaff(int accountTypeId)
        {
            var StaffRepo = _staffRepository.Repo;

            var result = await (from staff in StaffRepo
                                select new StaffDto
                                {
                                    Id = staff.Id,
                                    BranchId = staff.BranchId,
                                    BranchIdName = staff.Branch.Name,
                                    BranchIdStatus = staff.Branch.Status,
                                    PositionId = staff.PositionId,
                                    PositionIdName = staff.Position.Name,
                                    Name = staff.Name,
                                    BirthDate = staff.BirthDate,
                                    Email = staff.Email,
                                    Mobile = staff.Mobile,
                                    Home = staff.Home,
                                    Facebook = staff.Facebook,
                                    Instagram = staff.Instagram,
                                    Twitter = staff.Twitter,
                                    LinkedIn = staff.LinkedIn,
                                    BookingUrl = staff.BookingUrl,
                                    Address = staff.Address,
                                    StateId = staff.StateId,
                                    CityId = staff.CityId,
                                    Zip = staff.Zip,
                                    Avatar = staff.Avatar,
                                    Gender = staff.Gender,
                                    IdentityNumber = staff.IdentityNumber,
                                    Description = staff.Description,
                                    Status = staff.Status,
                                    CreatedStaffId = staff.CreatedStaffId,
                                    CreatedDate = staff.CreatedDate,
                                    UpdatedStaffId = staff.UpdatedStaffId,
                                    UpdatedDate = staff.UpdatedDate
                                })
                          .Where(c => c.Id == 1 && c.Status < (int)EStatus.All)
                          .ToFilterByAccountType(f => f.BranchIdStatus.GetValueOrDefault(),
                          f => f.Status.GetValueOrDefault(), accountTypeId)
                          .OrderBy(c => c.Id)
                          .FirstAsync();

            return result;
        }

        public async Task<Staff> GetById(int id)
        {
            var result = await _staffRepository.Repo
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Update(Staff model)
        {
            var result = false;
            var record = await _staffRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (record != null)
            {
                record.BranchId = model.BranchId;
                record.PositionId = model.PositionId;
                record.Name = model.Name;
                record.BirthDate = model.BirthDate;
                record.Email = model.Email;
                record.Mobile = model.Mobile;
                record.Home = model.Home;
                record.Facebook = model.Facebook;
                record.Instagram = model.Instagram;
                record.Twitter = model.Twitter;
                record.LinkedIn = model.LinkedIn;
                record.BookingUrl = model.BookingUrl;
                record.Address = model.Address;
                record.StateId = model.StateId;
                record.CityId = model.CityId;
                record.Zip = model.Zip;
                record.Avatar = model.Avatar;
                record.Description = model.Description;
                record.Gender = model.Gender;
                record.IdentityNumber = model.IdentityNumber;
                record.UpdatedDate = model.UpdatedDate;

                await _unitOfWork.SaveChangesAsync();

                result = true;
            }
            return result;
        }
    }
}
