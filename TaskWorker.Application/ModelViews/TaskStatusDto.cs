using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskStatusDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
