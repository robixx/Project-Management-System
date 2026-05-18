using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Infrastructure.DBConnection;

namespace TaskWorker.Infrastructure.Services
{
    public class NotificationService : INotification
    {
        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public NotificationService(DatabaseConnection connection, IHttpContextAccessor httpcontextaccessor)
        {
            _connection = connection;
            _httpcontextaccessor = httpcontextaccessor;
        }
        public async Task<(string Message, bool Status, List<NotificationDto> data)> SendNotification()
        {
            try
            {
                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
                var displayName = _httpcontextaccessor.HttpContext?.User?.FindFirst("DisplayName")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;
                var query= await _connection.Notification.Where(n => n.UserId == UserId)
                    .Select(n => new NotificationDto
                    {
                        Id = n.Id,
                        UserId = n.UserId,
                        Title = n.Title,
                        MessageValue = n.MessageValue,
                        FromUser = n.FromUser,
                        Isread = n.Isread,
                        CreatedAt = n.CreatedAt
                    }).AsNoTracking().ToListAsync();

                return ($"Data Retrieved for User {displayName}", true, query);

            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false, new List<NotificationDto>());
            }
            
        }
    }
}
