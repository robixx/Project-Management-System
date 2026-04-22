using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface IProject
    {
        Task<(string Message, bool Status, List<ProjectDto> project_list)> GetProjectListAsync();
        Task<(string Message, bool Status)> CreateProjectAsync(ProjectDto project);
    }
}
