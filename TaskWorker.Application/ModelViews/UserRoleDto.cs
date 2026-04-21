using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class UserRoleDto
    {
        public int UserId { get; set; }      
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string? ImageName { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; } 
        public int IsActive { get; set; }

    }
}
