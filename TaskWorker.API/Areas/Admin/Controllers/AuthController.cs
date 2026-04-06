using Microsoft.AspNetCore.Mvc;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/v1/[area]/[controller]")]
    public class AuthController : Controller
    {

        [HttpGet("auth-user")]
        public IActionResult Login_User()
        {
            return View();
        }
    }
}
