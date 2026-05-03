using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_TaskAssign")]
    public class AppTaskAssign
    {
        [Key]
        public int AssignId { get; set; }        
        public int TaskId { get; set; }

        public int? AssignTypeId { get; set; }

        public int? AssignedToUser { get; set; }

        public int? AssignedToTeam { get; set; }      
        public int AssignedBy { get; set; }

        public DateTime? AssignedAt { get; set; }

        public int? Status { get; set; }

        public string? Comments { get; set; } 
    }
}
