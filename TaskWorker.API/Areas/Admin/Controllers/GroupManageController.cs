using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupManageController : ControllerBase
    {
        private readonly IGroup _group;
        private readonly IBaseData _baseData;

        public GroupManageController(IGroup group, IBaseData baseData)
        {
            _group = group;
            _baseData = baseData;
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

        [HttpGet("get-team-list")]

        public async Task<IActionResult> GetTeamList()
        {
            var data = await _baseData.GetTeamListAsync();
            return Ok(new { Message = "Team list retrieved successfully", Status = true, data });
        }

        [HttpPost("add-team-member")]
        public async Task<IActionResult> AssignTeamMember([FromForm] AddGroupMemberDto addGroupMemberDto)
        {
            var (Message, Status) = await _group.AssignTeamMemberAsync(addGroupMemberDto);
            return Ok(new { Message, Status });
        }
    }
}
