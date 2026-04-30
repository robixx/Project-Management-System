using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public ProjectService(DatabaseConnection connection, IHttpContextAccessor httpcontextaccessor)
        {
            _connection = connection;
            _httpcontextaccessor = httpcontextaccessor;
        }


        public async Task<(string Message, bool Status, List<ProjectDto> data)> GetProjectListAsync()
        {
            try
            {
                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                var unitlist = await _connection
                       .Set<GetUnitDto>()
                       .FromSqlRaw("SELECT * FROM fn_get_departments_by_user({0});", UserId)
                       .ToListAsync();

                var unitIds = unitlist.Select(x => x.UnitId).ToList();

                var project_list = await _connection.AppProject
                    .Where(p => p.Status == 1 && p.Progress == 0 && unitIds.Contains(p.UnitId))
                    .Select(n => new ProjectDto
                    {
                        ProjectId = n.ProjectId,
                        ProjectName = n.ProjectName,
                        Description = n.Description,
                        CreatedBy = n.CreatedBy,
                        CreatedAt = n.CreatedAt,
                        Status = n.Status,
                        UnitId = n.UnitId,
                    }).ToListAsync();

                return ("Project Retrieved Successfully", true, project_list);

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
                if (project == null)
                {
                    return ("Project data is null", false);
                }

                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
                var unitId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UnitId")?.Value;

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
                        CreatedBy = Convert.ToInt32(userId),
                        UnitId = unitId != null ? Convert.ToInt32(unitId) : 0
                    };
                    await _connection.AppProject.AddAsync(newProject);
                }

                await _connection.SaveChangesAsync();
                return ("Project saved successfully", true);

            }
            catch (Exception ex)
            {
                return ($"Error creating project: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<IssueDto> data)> GetIssueListAsync()
        {
            try
            {
                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                var unitlist = await _connection
                       .Set<GetUnitDto>()
                       .FromSqlRaw("SELECT * FROM fn_get_departments_by_user({0});", UserId)
                       .ToListAsync();

                var unitIds = unitlist.Select(x => x.UnitId).ToList();

                var issue_list = await (from iu in _connection.AppIssue
                                        join pr in _connection.AppTaskPriority on iu.PriorityId equals pr.PriorityId
                                        join pj in _connection.AppProject
                                        .Where(p => unitIds.Contains(p.UnitId))
                                        on iu.ProjectId equals pj.ProjectId
                                        where iu.Status == 1
                                        select new IssueDto
                                        {

                                            IssueId = iu.IssueId,
                                            ProjectId = pj.ProjectId,
                                            IssueTitle = iu.IssueTitle,
                                            Description = iu.Description,
                                            ProjectName = pj.ProjectName,
                                            CreatedBy = iu.CreatedBy,
                                            TaskStatus = iu.TaskStatus,
                                            PriorityId= iu.PriorityId,
                                            Priority= pr.PriorityName,
                                            Status = iu.Status,
                                            CreateAt = iu.CreateAt
                                        }).ToListAsync();

                return ("Issues retrieved successfully", true, issue_list);
            }
            catch (Exception ex)
            {
                return ($"Error retrieving issues: {ex.Message}", false, new List<IssueDto>());
            }
        }

        public async Task<(string Message, bool Status)> CreateIssueAsync(IssueDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ("Issue data is null", false);
                }

                string msg = string.Empty;

                if (dto.IssueId > 0)
                {
                    var existingIssue = await _connection.AppIssue.FindAsync(dto.IssueId);
                    if (existingIssue == null)
                    {
                        return ($"Issue with ID {dto.IssueId} not found", false);
                    }
                    existingIssue.ProjectId = dto.ProjectId;
                    existingIssue.PriorityId = dto.PriorityId;
                    existingIssue.IssueTitle = dto.IssueTitle;
                    existingIssue.Description = dto.Description;
                    existingIssue.CreatedBy = dto.CreatedBy;
                    existingIssue.TaskStatus = dto.TaskStatus;
                    existingIssue.Status = dto.Status;
                    existingIssue.CreateAt = DateTime.Now;
                    msg = "Issue updated successfully";
                }
                else
                {
                    var newIssue = new AppIssue
                    {
                        ProjectId = dto.ProjectId,
                        IssueTitle = dto.IssueTitle,
                        PriorityId = dto.PriorityId,
                        Description = dto.Description,
                        CreatedBy = dto.CreatedBy,
                        TaskStatus = (int)TaskWorker.Infrastructure.Utility.TaskStatus.Pending,
                        Status = dto.Status,
                        CreateAt = DateTime.Now
                    };

                    await _connection.AppIssue.AddAsync(newIssue);
                    msg = "Issue created successfully";
                }

                await _connection.SaveChangesAsync();

                return (msg, true);
            }
            catch (Exception ex)
            {
                return ($"Error creating issue: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<AssignTypeDto> data)> GetAssignTypeListAsync()
        {
            try
            {
                var assignTypeList = await _connection.AppAssignType
                    .Where(a => a.IsActive == 1)
                    .Select(a => new AssignTypeDto
                    {
                        Id = a.Id,
                        TypeName = a.TypeName,
                        IsActive = a.IsActive,
                        CreatedDate = a.CreatedDate
                    })
                    .ToListAsync();

                return ("Assign type list retrieved successfully", true, assignTypeList);
            }
            catch (Exception ex)
            {
                return ($"Error retrieving assign type list: {ex.Message}", false, new List<AssignTypeDto>());
            }
        }

        public async Task<(string Message, bool Status)> CreateAssignTypeAsync(AssignTypeDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ("Assign type data is null", false);
                }
                string msg = string.Empty;
                if (dto.Id > 0)
                {
                    var existingAssignType = await _connection.AppAssignType.FindAsync(dto.Id);
                    if (existingAssignType == null)
                    {
                        return ($"Assign type with ID {dto.Id} not found", false);
                    }
                    existingAssignType.TypeName = dto.TypeName;
                    existingAssignType.IsActive = dto.IsActive;
                    existingAssignType.CreatedDate = DateTime.Now;

                    msg= "Assign type updated successfully";
                }
                else
                {
                    var newAssignType = new AppAssignType
                    {
                        TypeName = dto.TypeName,
                        IsActive = dto.IsActive,
                        CreatedDate = DateTime.Now
                    };
                    await _connection.AppAssignType.AddAsync(newAssignType);

                    msg= "Assign type created successfully";
                }

                await _connection.SaveChangesAsync();

                return (msg, true);

            }
            catch (Exception ex)
            {
                return ($"Error creating assign type: {ex.Message}", false);
            }
        }
    }
}
