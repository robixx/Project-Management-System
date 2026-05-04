using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskCloseDto
    {
        public int TaskId { get; set; }
        public int TaskStatus { get; set; }
    }
}
