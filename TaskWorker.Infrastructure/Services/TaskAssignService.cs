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
using TaskWorker.Infrastructure.Utility;

namespace TaskWorker.Infrastructure.Services
{
    public class TaskAssignService : ITaskboard
    {
        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public TaskAssignService(DatabaseConnection connection,IHttpContextAccessor httpContextAccessor)
        {
            _connection = connection;
            _httpcontextaccessor = httpContextAccessor;
        }
        public async Task<(string Message, bool Status)> AssignTaskAsync(TaskAssignDto taskAssignDto)
        {
            try
            {
                if(taskAssignDto == null)
                {
                    return ("Error: TaskAssignDto is null", false);
                }
                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                var assignType = taskAssignDto.AssignType?.ToLower();

                string msg=string.Empty;

                var entity = new AppTaskAssign
                {
                    TaskId = taskAssignDto.TaskId,
                    AssignTypeId = taskAssignDto.AssignTypeId,                    
                    AssignedBy = UserId,
                    AssignedAt = DateTime.Now,
                    Status = 1,
                    Comments = taskAssignDto.Remarks
                };

                // 👉 USER ASSIGNMENT
                if (assignType == "user")
                {
                    entity.AssignedToUser = taskAssignDto.AssignToUserOrToTeamId;
                    entity.AssignedToTeam = 0;
                    msg= "Task Assigned to User Successfully";
                }
                // 👉 TEAM ASSIGNMENT
                else if (assignType == "team")
                {
                    entity.AssignedToTeam = taskAssignDto.AssignToUserOrToTeamId;
                    entity.AssignedToUser = 0;
                    msg= "Task Assigned to Team Successfully";
                }
                else
                {
                    return ("Invalid Assign Type (use user or team)", false);
                }

                _connection.AppTaskAssign.Add(entity);
                await _connection.SaveChangesAsync();

                return (msg, true);

            } catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false);
            }
            
        }

        public async Task<(string Message, bool Status, List<TaskAssignmentDto> data)> GetTaskAssignmentAsync()
        {
            try
            {
                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                var entities = await _connection.TaskAssignment.FromSqlRaw("Select * from public.get_task_assignments({0})", UserId)
                    .AsNoTracking()
                    .ToListAsync();

                var dataDto = entities.Select(x => new TaskAssignmentDto
                {
                    AssignId = x.AssignId,
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectName,
                    TaskId = x.TaskId,
                    IssueTitle = x.IssueTitle,
                    AssignTypeId = x.AssignTypeId,
                    TypeName = x.TypeName,

                    AssignedToUser = x.AssignedToUser,
                    AssignedToTeam = x.AssignedToTeam,
                    AssignedBy = x.AssignedBy,

                    UserName = x.UserName,
                    AssignedPerson = x.AssignedPerson,
                    PersonImage = x.PersonImage,

                    AssignedTeam = x.AssignedTeam,
                    TeamImage = x.TeamImage,

                    AssignedAt = x.AssignedAt,
                    Status = x.Status,
                    Comments = x.Comments,

                    TaskStatus = ((TaskStatusEnum)x.TaskStatus) switch
                    {
                        TaskStatusEnum.Pending => "Pending",
                        TaskStatusEnum.InProgress => "In Progress",
                        TaskStatusEnum.Completed => "Completed",
                        _ => "Unknown"
                    },
                    PriorityId = x.PriorityId,
                    PriorityName = x.PriorityName

                }).ToList();

                return ("Data retrieved successfully", true, dataDto);

            }
            catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false, new List<TaskAssignmentDto>());
            }
        }

        
        public async Task<(string Message, bool Status)> TransferTaskAsync(TaskTransferDto transferDto)
        {
            using var transaction = await _connection.Database.BeginTransactionAsync();

            try
            {
                var taskAssign = await _connection.AppTaskAssign
                    .FirstOrDefaultAsync(x => x.AssignId == transferDto.AssignId);

                if (taskAssign == null)
                    return ("Task not found", false);

                // Only pending allowed
                if (taskAssign.Status != 1)
                    return ("Only pending tasks can be transferred", false);

                // 1. Insert history
                var history = new TaskTransferHistory
                {
                    TaskId = transferDto.AssignId,
                    FromUserId = taskAssign.AssignedToUser,
                    FromTeamId = taskAssign.AssignedToTeam,
                    ToUserId = transferDto.ToUserId,
                    ToTeamId = transferDto.ToTeamId,
                    TransferredBy = transferDto.TransferredBy,
                    TransferDate = DateTime.Now,
                    Comments = transferDto.Comments
                };

                await _connection.TaskTransferHistory.AddAsync(history);

                // 2. Update assignment
                taskAssign.AssignedToUser = transferDto.ToUserId;
                taskAssign.AssignedToTeam = transferDto.ToTeamId;
                taskAssign.AssignedBy = transferDto.TransferredBy;
                taskAssign.AssignedAt = DateTime.Now;

                await _connection.SaveChangesAsync();

                // 3. Commit transaction
                await transaction.CommitAsync();

                return ("Task transferred successfully", true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ($"Error: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<TaskTransferHistoryViewDto> data)> TaskTransferListAsync()
        {
            try
            {
                var data = await _connection
                       .Set<TaskTransferHistoryViewDto>()
                       .FromSqlRaw("SELECT * FROM get_task_transfer_history();")
                       .ToListAsync();

                return ("Data Retrive Successfully", true, data);
            }catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false, new List<TaskTransferHistoryViewDto>());
            }
        }

    }
}
