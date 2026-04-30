using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    public class TaskAssignment
    {
        public int AssignId { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int TaskId { get; set; }
        public string? IssueTitle { get; set; }
        public int AssignTypeId { get; set; }
        public string? TypeName { get; set; }
        public int AssignedToUser { get; set; }
        public int AssignedToTeam { get; set; }
        public int AssignedBy { get; set; }
        public string? UserName { get; set; }
        public string? AssignedPerson { get; set; }
        public string? PersonImage { get; set; }
        public string? AssignedTeam { get; set; }
        public string? TeamImage { get; set; }
        public DateTime AssignedAt { get; set; }
        public int Status { get; set; }
        public string? Comments { get; set; }
        public int TaskStatus { get; set; }
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
    }
}
