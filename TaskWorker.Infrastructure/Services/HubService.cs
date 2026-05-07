using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TaskWorker.Infrastructure.Middleware;


namespace TaskWorker.Infrastructure.Services
{
    public class HubService
    {
        private readonly IHubContext<ProjectHub> _hubContext;

        public HubService(IHubContext<ProjectHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyClients(string method, string message)
        {
            await _hubContext.Clients.All.SendAsync(method, message);
        }

        public async Task NotifyProjectUpdate(string message)
        {
            await NotifyClients("ReceiveProjectUpdate", message);
        }

        public async Task NotifyIssueAssignment(string message)
        {
            await NotifyClients("ReceiveIssueAssignment", message);
        }

        public async Task NotifyReview(string message)
        {
            await NotifyClients("ReceiveReview", message);
        }

        public async Task NotifyTransfer(string message)
        {
            await NotifyClients("ReceiveTransfer", message);
        }

        public async Task NotifyClose(string message)
        {
            await NotifyClients("ReceiveClose", message);
        }
    }
}