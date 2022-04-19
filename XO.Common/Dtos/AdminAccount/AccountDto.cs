using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.AdminAccount
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string TypeIdName { get; set; }
        public int StaffId { get; set; }
        public string StaffIdName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }
        public int StateId { get; set; }
        public string StateIdName { get; set; }
        public int CityId { get; set; }
        public string CityIdName { get; set; }
        public int Zip { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public int? Active { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedStaffId { get; set; }
    }
}
