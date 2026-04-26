using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    public class GroupManageController : ControllerBase
    {
        private readonly IGroup _group;

        public GroupManageController(IGroup group)
        {
            _group = group;
        }

        [HttpGet("team-list")]
        public async Task<IActionResult> GetGroups()
        {
           var (Message, Status, data) = await _group.GetGroupListAsync();

            return Ok(new { Message, Status, data });
        }

        [HttpPost("add-team")]
        public async Task<IActionResult> AddTeam([FromForm] TeamDto teamDto)
        {
            var (Message, Status) = await _group.AddTeamAsync(teamDto);

            return Ok(new { Message, Status });
        }
    }
}
