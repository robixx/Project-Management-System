using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class fileshareController : ControllerBase
    {


        [HttpPost("file-share")]
        public async Task<IActionResult> ShareFile()
        {
            
            return Ok(new { Message = "File shared successfully", Status = true });
        }
    }
}
