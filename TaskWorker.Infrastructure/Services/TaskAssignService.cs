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

        public Task<(string Message, bool Status, List<TaskAssignmentDto> data)> GetTaskAssignmentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
