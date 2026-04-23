using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;
using TaskWorker.Infrastructure.DBConnection;
using TaskWorker.Infrastructure.Utility;

namespace TaskWorker.Infrastructure.Services
{
    public class UserInfoService : IUserInfo
    {
        private readonly DatabaseConnection _connection;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public UserInfoService(DatabaseConnection connection, IConfiguration config,
        IWebHostEnvironment env)
        {
            _connection = connection;
            _config = config;
            _env = env;
        }

        
        public async Task<(string Message, bool Status)> SaveUserAsync(AppUserDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ("User data is empty", false);
                }

                string? fileName = null;

                string folderPath = _config["FileStorage:UserImagePath"] ?? "D:\\TaskWorker";

                string uploadPath = Path.Combine(_env.ContentRootPath, folderPath);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
               
                if (dto.ImageName != null && dto.ImageName.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageName.FileName);

                    string fullPath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);                    
                        await dto.ImageName.CopyToAsync(stream);
                    
                }

                var user = new AppUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    ImageName = fileName,
                    DepartmentId = dto.DepartmentId,
                    DesignationId = dto.DesignationId,
                    IsActive = dto.IsActive??0,
                    CreatedAt = DateTime.Now
                };

                await _connection.AppUser.AddAsync(user);
                await _connection.SaveChangesAsync();

                return ($"{dto.UserName} saved successfully", true);
            }
            catch (Exception ex)
            {
                return ($"Error occurred: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status)> SaveUserRegisterAsync(AppSecUserDto dto)
        {
            await using var transaction = await _connection.Database.BeginTransactionAsync();

            try
            {
                if (dto == null)
                    return ("User data is empty", false);

                if (dto.UserId == 0)
                    return ("User Not Selected ", false);

                var userId = await _connection.AppUser.FirstOrDefaultAsync(i => i.UserId == dto.UserId && i.IsActive==1);
                if (userId == null)
                    return ("Invalid User", false);

                string msg = string.Empty;
                AppSecUser? user;

                // ================= UPDATE =================
                if (dto.Id > 0)
                {
                    user = await _connection.AppSecUser
                        .FirstOrDefaultAsync(x => x.Id == dto.Id);
                       
                    if (user == null)
                        return ("User not found", false);

                    user.UserId = dto.UserId ?? 0;
                    user.LoginName = dto.LoginName;
                    user.UnitId = userId.DepartmentId??0;

                    if (!string.IsNullOrWhiteSpace(dto.HashPassword))
                    {
                        user.HashPassword = PasswordHelper.Hash(dto.HashPassword);
                    }

                    user.IsActive = dto.IsActive;
                    user.LastLoginDate = DateTime.Now;
                    msg = $"{dto.LoginName} updated successfully";
                }
                // ================= INSERT =================
                else
                {
                    user = new AppSecUser
                    {
                        UserId = dto.UserId??0,
                        LoginName = dto.LoginName,
                        HashPassword = PasswordHelper.Hash(dto.HashPassword ?? string.Empty),
                        LastLoginDate = DateTime.Now,
                        IsActive = dto.IsActive,
                        UnitId = userId.DepartmentId ?? 0,
                    };

                    await _connection.AppSecUser.AddAsync(user);
                    msg = $"{dto.LoginName} saved successfully";
                }

                await _connection.SaveChangesAsync();

                // ================= BACKUP TABLE =================
                var backup = new AppEncryptedData
                {
                    OwnerId = user.UserId,
                    EncriptedData = EncryptionHelper.MakeEncryptedData(dto.HashPassword ?? string.Empty)
                };

                await _connection.AppEncryptedData.AddAsync(backup);
                await _connection.SaveChangesAsync();

                // ================= COMMIT =================
                await transaction.CommitAsync();

                return (msg, true);
            }
            catch (Exception ex)
            {
                // ================= ROLLBACK =================
                await transaction.RollbackAsync();

                return ($"Error occurred: {ex.Message}", false);
            }
        }

        public class EncryptionHelper
        {
            public static string MakeEncryptedData(string data)
            {
                var prefix = new Random().Next(1000, 9999);
                var suffix = new Random().Next(1000, 9999);

                return $"{prefix}-{data}-{suffix}";
            }
        }



        public async Task<LoginResponseDto> GetloginUser(int userId)
        {
            try
            {
                LoginResponseDto userlist = new();
                var roleid = await _connection.AppUserRole
                    .Where(i => i.UserId == userId)
                    .Select(i => i.RoleId)
                    .FirstOrDefaultAsync();
                var roleName = await _connection.AppRole
                    .Where(r => r.RoleId == roleid)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();

                var result = await _connection.AppUser
                    .Where(u => u.IsActive == 1 && u.UserId == userId)
                    .Select(u => new LoginResponseDto
                    {
                        UserId = u.UserId,
                        DisplayName = u.UserName,
                        RoleId = roleid,
                        RoleName = roleName ?? "",
                        UnitId= u.DepartmentId??0,
                    })
                    .FirstOrDefaultAsync();


                if (result != null)
                {
                    userlist = result;
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred: {ex.Message}");
            }

        }

        public async Task<(string Message, bool Status, List<UserRoleDto> Data)> GetUserListAsync()
        {
            try
            {
                var list = await _connection
                       .Set<UserRoleDto>()
                       .FromSqlRaw("SELECT * FROM fn_get_user_details();")
                       .ToListAsync();


                return ("User Retrived Successfully", true, list);
               
            }
            catch (Exception ex)
            {
                return ($"Error occurred: {ex.Message}", false, new List<UserRoleDto>());
            }
        }
    }
}
