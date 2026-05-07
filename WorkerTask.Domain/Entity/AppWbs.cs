using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    public class AppWbs
    {
        public int WbsId { get; set; }
        public string? WbsName { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
