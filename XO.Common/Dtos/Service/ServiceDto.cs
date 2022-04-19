using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Dtos.Service
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeIdName { get; set; }
        public int? ServiceTypeIdStatus { get; set; }
        public int? SubServiceId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }
        public string Icon { get; set; }
        public string Times { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? CreatedStaffId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedStaffId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ServiceDto> SubService { get; set; }
    }
}
