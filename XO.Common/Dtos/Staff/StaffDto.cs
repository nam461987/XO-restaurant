using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.Staff
{
    public class StaffDto
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string BranchIdName { get; set; }
        public int? BranchIdStatus { get; set; }
        public int? PositionId { get; set; }
        public string PositionIdName { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Home { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string BookingUrl { get; set; }
        public string Address { get; set; }
        public int? StateId { get; set; }
        public string StateIdName { get; set; }
        public int? CityId { get; set; }
        public string CityIdName { get; set; }
        public int? Zip { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedStaffId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedStaffId { get; set; }
    }
}
