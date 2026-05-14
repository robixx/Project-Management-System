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
        public IActionResult Index()
        {
            return View();
        }
    }
}
