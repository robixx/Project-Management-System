using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class fileshareController : ControllerBase
    {
        private readonly IFileShare _fileShare;

        public fileshareController(IFileShare fileShare)
        {
            _fileShare = fileShare;
        }



        [HttpPost("file-share")]
        public async Task<IActionResult> ShareFile([FromBody] FileShareDto fileshare)
        {
            var (Message, Status) = await _fileShare.ShareFileAsync(fileshare);

            return Ok(new { Message, Status  });
        }
    }
}
