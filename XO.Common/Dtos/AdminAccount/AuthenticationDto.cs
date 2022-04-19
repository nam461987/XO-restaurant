using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.AdminAccount
{
    public class AuthenticationDto
    {
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public int StaffId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
