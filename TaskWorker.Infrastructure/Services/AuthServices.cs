using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Infrastructure.DBConnection;
using TaskWorker.Infrastructure.Utility;

namespace TaskWorker.Infrastructure.Services
{
    public class AuthServices : IAuth
    {
        private readonly DatabaseConnection _connection;

        public AuthServices(DatabaseConnection connection)
        {
            _connection = connection;
        }
        public async Task<LoginResponseDto> AuthenticateAsync(LoginReqquest loginRequest)
        {
            try
            {
                var user = await _connection.AppSecUser
                    .Where(u => u.LoginName == loginRequest.loginName && u.IsActive == 1)
                    .Select(u => new
                    {
                        u.LoginName,
                        u.HashPassword,
                        u.UserId
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return new LoginResponseDto();
                }

                bool isVerified = PasswordHelper.Verify(user.HashPassword??"", loginRequest.password);

                if (!isVerified)
                {
                    return new LoginResponseDto();
                }

                var role = await _connection.AppUserRole
                    .FirstOrDefaultAsync(r => r.UserId == user.UserId);

                var lrole = role != null
                    ? await _connection.AppRole.FirstOrDefaultAsync(r => r.RoleId == role.RoleId)
                    : null;

                return new LoginResponseDto
                {
                    DispalyName = user.LoginName,
                    UserId = user.UserId,
                    RoleName = lrole?.RoleName,
                    RoleId = role?.RoleId ?? 0
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return new LoginResponseDto();
            }
        }
    }
}
