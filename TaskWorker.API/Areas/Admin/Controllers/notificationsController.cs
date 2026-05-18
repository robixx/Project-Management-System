using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class notificationsController : Controller
    {
        private readonly INotification _notification;
        public notificationsController(INotification notification)
        {
            _notification = notification;
        }

        [HttpGet("get-notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var (Message, Status, data) = await _notification.SendNotification();
            return Ok(new {Message,Status,data});
        }
    }
}
