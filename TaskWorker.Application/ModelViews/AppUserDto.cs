using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class AppUserDto
    {
        public int UserId { get; set; }
       
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public int? DepartmentId { get; set; }

        public int? DesignationId { get; set; }

        public IFormFile? ImageName { get; set; }

        public int? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
