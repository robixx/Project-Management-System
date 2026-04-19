using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Infrastructure.Services
{
    public class AuthServices : IAuth
    {
        public Task<LoginResponseDto> AuthenticateAsync(LoginReqquest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
