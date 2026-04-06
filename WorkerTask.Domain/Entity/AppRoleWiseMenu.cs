using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_RoleWise_Menu")]
    public class AppRoleWiseMenu
    {
        [Key]
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? MenuId { get; set; } 

        public int? IsActive { get; set; }
    }
}
