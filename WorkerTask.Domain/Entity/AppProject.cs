using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Projects")]
    public class AppProject
    {
        [Key]
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }

        public int UnitId { get; set; }
        public int Progress { get; set; }
    }
}
