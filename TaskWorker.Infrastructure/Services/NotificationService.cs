using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Infrastructure.Services
{
    public class NotificationService : INotification
    {
        public Task<(string Message, string Status, List<NotificationDto> data)> SendNotification()
        {
            throw new NotImplementedException();
        }
    }
}
