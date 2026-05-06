using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Infrastructure.Services
{
    public class DocShareService : IDocShare
    {
        public Task<(string Message, bool Status)> ShareDocumentAsync(FileShareDto fileShareDto)
        {
            throw new NotImplementedException();
        }
    }
}
