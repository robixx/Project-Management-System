using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectManageController : Controller
    {

        private readonly IProject _project;

        public ProjectManageController(IProject project)
        {
            _project = project;
        }


        [HttpGet("get-project-list")]
        public async Task<IActionResult> ProjectList()
        {
            var (Message, Status, data) = await _project.GetProjectListAsync();
            return Ok(new { Message, Status, data });
        }


        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDto project)
        {
            var (Message, Status) = await _project.CreateProjectAsync(project);
            return Ok(new { Message, Status });
        }

        [HttpGet("issue-list")]
        public async Task<IActionResult> IssueList()
        {
            var (Message, Status, data) = await _project.GetIssueListAsync();
            return Ok(new { Message, Status, data });
        }
    }
}
