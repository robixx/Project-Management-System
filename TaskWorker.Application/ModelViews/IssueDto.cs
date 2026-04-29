using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class IssueDto
    {
        public int IssueId { get; set; }

        public int? ProjectId { get; set; }
        public int PriorityId { get; set; }

        [MaxLength(500)]
        public string? IssueTitle { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public string? Priority { get; set; }

        public int? CreatedBy { get; set; }

        public int? AssignedTo { get; set; }

        public int? Status { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}
