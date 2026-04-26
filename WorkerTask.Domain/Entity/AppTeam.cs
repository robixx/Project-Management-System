using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Teams")]
    public class AppTeam
    {
        [Key]
        public int TeamId { get; set; } 

        [MaxLength(250)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? TeamImage { get; set; } 
        public int? CreatedBy { get; set; } 
        public int IsActive { get; set; } 
        public DateTime? CreatedAt { get; set; }
    }
}
