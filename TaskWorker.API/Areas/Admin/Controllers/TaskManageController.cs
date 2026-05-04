using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;


namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskManageController : ControllerBase
    {

        private readonly ITaskboard _task;

        public TaskManageController(ITaskboard task)
        {
            _task = task;
        }


        [HttpPost("task-assign")]
        public async Task<IActionResult> AssignTask([FromBody] TaskAssignDto assignDto)
        {
            var (Message, Status) = await _task.AssignTaskAsync(assignDto);
            return Ok(new { Message, Status });
        }


        [HttpGet("task-list")]
        public async Task<IActionResult> GetTaskList()
        {

            var (Message, Status, data) = await _task.GetTaskAssignmentAsync();

            return Ok(new { Message, Status, data });
        }


        [HttpPost("task-transfer")]
        public async Task<IActionResult> TransferTask([FromBody] TaskTransferDto transferDto)
        {
            var (Message, Status) = await _task.TransferTaskAsync(transferDto);
            return Ok(new { Message, Status });
        }



        [HttpGet("task-transfer-list")]
        public async Task<IActionResult> TaskTransferlist()
        {

            var (Message, Status, data) = await _task.TaskTransferListAsync();
            return Ok(new { Message, Status, data });
        }


        [HttpPost("task-close")]
        public async Task<IActionResult> CloseTask(int TaskId)
        {
            var (Message, Status) = await _task.CloseTaskAsync(TaskId);
            return Ok(new { Message, Status });
        }

        [HttpPost("file-upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            var (Message, Status) = await _task.UploadFileAsync(dto);

            return Ok(new { Message, Status });
        }
    }
}
