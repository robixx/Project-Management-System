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
    public class ProjectService : IProject
    {
        private readonly DatabaseConnection _connection;

        public ProjectService(DatabaseConnection connection)
        {
            _connection = connection;
        }
        public Task<(string Message, bool IsSuccess, List<ProjectDto> project_list)> CreateProject()
        {
            throw new NotImplementedException();
        }
    }
}
