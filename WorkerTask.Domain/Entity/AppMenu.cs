using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Menu")]
    public class AppMenu
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string? MenuTitle { get; set; }

        [MaxLength(250)]
        public string? Urls { get; set; }

        [MaxLength(100)]
        public string? ControllerName { get; set; }

        [MaxLength(100)]
        public string? ActionName { get; set; }
        [MaxLength(100)]
        public string? PageArea { get; set; }

        public int? IsParent { get; set; }

        [MaxLength(100)]
        public string? Icon { get; set; }

        public int IsActive { get; set; } = 1; 
    }
}
