using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Middleware
{
    public class NotificationHub : Hub
    {
        public async Task ReceiveNotification(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", title, message);
        }

        public async Task NotifyIssueAssignment(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveIssueAssignment", message);
        }

        public async Task NotifyReview(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveReview", message);
        }

        public async Task NotifyTransfer(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveTransfer", message);
        }

        public async Task NotifyClose(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveClose", message);
        }
    }
}