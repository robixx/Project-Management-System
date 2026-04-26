using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class GroupService : IGroup
    {

        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpcontextaccessor;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public GroupService(DatabaseConnection connection, IHttpContextAccessor httpcontextaccessor, IWebHostEnvironment env, IConfiguration config)
        {
            _connection = connection;
            _httpcontextaccessor = httpcontextaccessor;
            _env = env;
            _config = config;
        }

        public async Task<(string Message, bool Status)> AddTeamAsync(TeamDto teamDto)
        {
            try
            {
                if (teamDto == null)
                    return ("Team data is null", false);

                string? fileName = null;
                string msg = string.Empty;

                string folderPath = _config["FileStorage:UserImagePath"] ?? "D:\\TaskWorker";
                string uploadPath = Path.Combine(_env.ContentRootPath, folderPath);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                // =========================
                // FILE UPLOAD
                // =========================
                if (teamDto.TeamImage != null && teamDto.TeamImage.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(teamDto.TeamImage.FileName);

                    string fullPath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await teamDto.TeamImage.CopyToAsync(stream);
                }

                // =========================
                // CREATE OR UPDATE
                // =========================

                if (teamDto.TeamId > 0)
                {
                    // UPDATE (SAFE + NO WARNING)
                    var team = await _connection.AppTeam
                        .FirstOrDefaultAsync(t => t.TeamId == teamDto.TeamId);

                    if (team == null)
                        return ("Team not found", false);

                    team.Name = teamDto.Name;
                    team.CreatedBy = UserId;
                    team.CreatedAt = DateTime.Now;

                    if (fileName != null)
                        team.TeamImage = fileName;

                    msg = $"{teamDto.Name} Team updated successfully";
                }
                else
                {
                    // CREATE
                    var team = new AppTeam
                    {
                        Name = teamDto.Name,
                        TeamImage = fileName,
                        CreatedBy = UserId,
                        CreatedAt = DateTime.Now,
                        IsActive = 1
                    };

                    await _connection.AppTeam.AddAsync(team);

                    msg = $"{teamDto.Name} Team created successfully";
                }

                await _connection.SaveChangesAsync();

                return (msg, true);

            }
            catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<TeamDto> data)> GetGroupListAsync()
        {
            try
            {

                var query = await _connection.AppTeam
                            .Where(i => i.IsActive == 1)
                            .Select(i => new TeamDto
                            {
                                TeamId = i.TeamId,
                                Name = i.Name,
                                ImageName = i.TeamImage,
                                CreatedBy = i.CreatedBy,
                                CreatedAt = i.CreatedAt,
                                IsActive = i.IsActive
                            })
                            .ToListAsync();

               
                return ("Team list retrieved successfully", true, query.ToList());

            }
            catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false, new List<TeamDto>());
            }
        }
    }
}
