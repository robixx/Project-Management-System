using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Tasks")]
    public class AppTask
    {
        [Key]
        public int TasksId { get; set; }

        public int? IssueId { get; set; }

        [MaxLength(500)]
        public string? TaskTitle { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } 

        public int? CreatedBy { get; set; } 

        public int? Status { get; set; } 

        public int? Priority { get; set; } 

        public DateTime? CreatedAt { get; set; } 
    }
}
