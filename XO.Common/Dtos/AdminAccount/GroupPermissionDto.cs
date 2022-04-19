using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.AdminAccount
{
    public class GroupPermissionDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }
        public string GroupIdName { get; set; }
        public string PermissionIdName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
