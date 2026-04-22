using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface IUserInfo
    {

        Task<(string Message, bool Status)>SaveUserAsync(AppUserDto dto);
        Task<(string Message, bool Status)>SaveUserRegisterAsync(AppSecUserDto dto);
        Task<LoginResponseDto> GetloginUser(int userId);

    }
}
