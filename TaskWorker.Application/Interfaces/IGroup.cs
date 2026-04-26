using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface IGroup
    {
        Task<(string Message, bool Status, List<TeamDto> data)> GetGroupListAsync();
        Task<(string Message, bool Status)> AddTeamAsync(TeamDto teamDto);
    }
}
