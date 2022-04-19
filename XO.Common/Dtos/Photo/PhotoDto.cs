using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.Photo
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceIdName { get; set; }
        public int? ServiceIdStatus { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }
    }
}
