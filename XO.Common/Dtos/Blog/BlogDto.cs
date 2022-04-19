using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.Blog
{
    public class BlogDto
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string TypeIdName { get; set; }
        public int TypeIdStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NewsContent { get; set; }
        public DateTime? PostedDate { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public int? HotNews { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedStaffId { get; set; }
        public string CreatedStaffIdName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedStaffId { get; set; }
        public string UpdatedStaffIdName { get; set; }
    }
}
