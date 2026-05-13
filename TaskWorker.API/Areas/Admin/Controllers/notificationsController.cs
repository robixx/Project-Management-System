using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class notificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
