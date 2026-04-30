using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskTransferDto
    {

        public int TaskId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string? Remarks { get; set; }
    }
}
