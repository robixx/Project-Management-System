using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TaskAssignDto
    {
        public int TaskId { get; set; }
        public string? AssignType { get; set; }
        public int AssignTypeId { get; set; }
        public int AssignToUserOrToTeamId { get; set; }
        public string? Remarks { get; set; }
    }
}
