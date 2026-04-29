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
        Task<(string Message, bool Status, List<ProjectDto> data)> GetProjectListAsync();       
        Task<(string Message, bool Status)> CreateProjectAsync(ProjectDto project);
        Task<(string Message, bool Status, List<IssueDto> data)> GetIssueListAsync();
        Task<(string Message, bool Status, List<AssignTypeDto> data)> GetAssignTypeListAsync();
        Task<(string Message, bool Status)> CreateIssueAsync(IssueDto dto);

    }
}
