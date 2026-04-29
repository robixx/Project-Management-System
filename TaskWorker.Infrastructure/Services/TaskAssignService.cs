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
    public class TaskAssignService : ITaskboard
    {
        private readonly DatabaseConnection _connection;

        public TaskAssignService(DatabaseConnection connection)
        {
            _connection = connection;
        }
        public async Task<(string Message, bool Status)> AssignTaskAsync(TaskAssignDto taskAssignDto)
        {
            
            throw new NotImplementedException();
        }
    }
}
