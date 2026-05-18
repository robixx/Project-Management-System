using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Middleware
{
    public class NotificationHub : Hub
    {
        public async Task NotifyProjectUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveProjectUpdate", message);
        }

        public async Task NotifyIssueAssignment(string message)
        {
            await Clients.All.SendAsync("ReceiveIssueAssignment", message);
        }

        public async Task NotifyReview(string message)
        {
            await Clients.All.SendAsync("ReceiveReview", message);
        }

        public async Task NotifyTransfer(string message)
        {
            await Clients.All.SendAsync("ReceiveTransfer", message);
        }

        public async Task NotifyClose(string message)
        {
            await Clients.All.SendAsync("ReceiveClose", message);
        }
    }
}