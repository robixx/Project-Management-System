using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;
using TaskWorker.Infrastructure.DBConnection;

namespace TaskWorker.Infrastructure.Services
{
    public class ProjectService : IProject
    {
        private readonly DatabaseConnection _connection;

        public ProjectService(DatabaseConnection connection)
        {
            _connection = connection;
        }

       
        public async Task<(string Message, bool Status, List<ProjectDto> data)> GetProjectListAsync()
        {
            try
            {
                var project_list = await _connection.AppProject
                    .Where(p=>p.Status == 1)
                    .Select(n => new ProjectDto
                    {
                        ProjectId = n.ProjectId,
                        ProjectName = n.ProjectName,
                        Description = n.Description,
                        CreatedBy = n.CreatedBy,
                        CreatedAt = n.CreatedAt,
                        Status = n.Status
                    }).ToListAsync();

                return("Project Retrieved Successfully", true, project_list);

            }
            catch (Exception ex)
            {
                return ($"Error creating project: {ex.Message}", false, new List<ProjectDto>());
            }
        }

        public async Task<(string Message, bool Status)> CreateProjectAsync(ProjectDto project)
        {
            try
            {
                if(project == null)
                {
                    return ("Project data is null", false);
                }
                if (project.ProjectId > 0)
                {
                    var existingProject = await _connection.AppProject.FindAsync(project.ProjectId);
                    if (existingProject == null)
                    {
                        return ($"Project with ID {project.ProjectId} not found", false);
                    }
                    existingProject.ProjectName = project.ProjectName;
                    existingProject.Description = project.Description;
                    existingProject.Status = project.Status;
                    existingProject.CreatedAt = DateTime.Now;
                    existingProject.CreatedBy = project.CreatedBy;
                }
                else
                {
                    var newProject = new AppProject
                    {
                        ProjectName = project.ProjectName,
                        Description = project.Description,
                        Status = project.Status,
                        CreatedAt = DateTime.Now,
                        CreatedBy = project.CreatedBy
                    };
                   await _connection.AppProject.AddAsync(newProject);
                }

                await _connection.SaveChangesAsync();
                return ("Project saved successfully", true);

            }
            catch(Exception ex)
            {
               return($"Error creating project: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<IssueDto> data)> GetIssueListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
