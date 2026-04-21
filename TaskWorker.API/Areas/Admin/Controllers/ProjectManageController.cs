using Microsoft.AspNetCore.Mvc;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    public class ProjectManageController : Controller
    {

        [HttpGet("get-project-list")]
        public async Task<IActionResult> ProjectList()
        {
            return View();
        }
    }
}
