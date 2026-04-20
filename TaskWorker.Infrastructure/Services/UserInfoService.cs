using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;
using TaskWorker.Infrastructure.DBConnection;

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

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.ImageName.CopyToAsync(stream);
                    }
                }

                var user = new AppUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    ImageName = fileName,
                    DepartmentId = dto.DepartmentId,
                    DesignationId = dto.DesignationId,
                    IsActive = dto.IsActive,
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
    }
}
