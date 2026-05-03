using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_TaskTransferHistory")]
    public class TaskTransferHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        public int? FromUserId { get; set; }

        public int? ToUserId { get; set; }

        public int? FromTeamId { get; set; }

        public int? ToTeamId { get; set; }       
        public int TransferredBy { get; set; }       
        public DateTime TransferDate { get; set; } = DateTime.Now;        
        public string? Comments { get; set; }
    }
}
