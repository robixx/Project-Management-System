using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface ITaskboard
    {
        Task<(string Message, bool Status)> AssignTaskAsync(TaskAssignDto assignDto);
    }
}
