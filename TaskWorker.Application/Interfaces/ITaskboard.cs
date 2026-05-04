using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface ITaskboard
    {
        Task<(string Message, bool Status)> AssignTaskAsync(TaskAssignDto assignDto);
        Task<(string Message, bool Status,List<TaskAssignmentDto> data)> GetTaskAssignmentAsync();
        Task<(string Message, bool Status,List<TaskTransferHistoryViewDto> data)> TaskTransferListAsync();
        Task<(string Message, bool Status)> TransferTaskAsync(TaskTransferDto transferDto);
        Task<(string Message, bool Status)> CloseTaskAsync(int TaskId);
        Task<(string Message, bool Status)> UploadFileAsync(FileUploadDto dto);
    }
}
