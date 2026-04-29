using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Task_Priority")]
    public class AppTaskPriority
    {
        [Key]
        public int PriorityId { get; set; }

        public string? PriorityName { get; set; }
    }
}
