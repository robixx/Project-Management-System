using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskTransferHistoryDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public int? FromTeamId { get; set; }
        public int? ToTeamId { get; set; }
        public int TransferredBy { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Comment { get; set; }
    }
}
