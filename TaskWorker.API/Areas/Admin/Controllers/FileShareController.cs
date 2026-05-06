using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class FileShareController : ControllerBase
    {
    }
}
