using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskTransferHistoryViewDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? FromUserName { get; set; }                  
        public string? ToUserName { get; set; }                     
        public string? FromTeamName { get; set; }                   
        public string? ToTeamName { get; set; }                
        public string? TransferUser { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Comments { get; set; }
    }
}
