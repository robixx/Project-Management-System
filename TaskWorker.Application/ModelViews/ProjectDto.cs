using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }
        public int UnitId { get; set; }
    }
}

