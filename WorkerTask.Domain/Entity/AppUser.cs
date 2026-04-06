using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{

    [Table("app_User")]
    public class AppUser
    {
        [Key]
        public int UserId { get; set; } 

        public string? UserName { get; set; } 

        public string? Email { get; set; } 

        public string? HashPassword { get; set; } 

        public int? DepartmentId { get; set; } 

        public int? DesignationId { get; set; } 

        public string? ImageName { get; set; }

        public int? IsActive { get; set; } 

        public DateTime? CreatedAt { get; set; } 
    }
}
