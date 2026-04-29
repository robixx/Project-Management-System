using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Task_Status")]
    public class AppTaskStatus
    {
        [Key]
        public int Id { get; set; }

        public int? StatusId { get; set; }

        public string? StatusName { get; set; }
    }
}
