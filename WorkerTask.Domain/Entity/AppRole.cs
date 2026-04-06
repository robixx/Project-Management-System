using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Role")]
    public class AppRole
    {
        [Key]
        public int RoleId { get; set; } 

        [MaxLength(50)]
        public string? RoleName { get; set; }

        public int? IsActive { get; set; } 
    }
}
