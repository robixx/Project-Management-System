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

        public async Task<(string Message, bool Status)> AssignTeamMemberAsync(AddGroupMemberDto addGroupMemberDto)
        {
            try
            {
                if (addGroupMemberDto == null)
                    return ("AddGroupMemberDto is null", false);

                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int UserId = int.TryParse(userId, out int parsedUserId) ? parsedUserId : 0;

                if(addGroupMemberDto.MemberId>0)
                {
                    var existingMember = await _connection.AppGroupTeam
                        .FirstOrDefaultAsync(m => m.MemberId == addGroupMemberDto.MemberId);
                    if (existingMember == null)
                        return ("Team member not found", false);
                    existingMember.TeamId = addGroupMemberDto.TeamId;
                    existingMember.UserId = addGroupMemberDto.UserId;
                    existingMember.JoinedAt = DateTime.Now;
                    existingMember.CreatedBy = UserId;
                    await _connection.SaveChangesAsync();
                    return ("Team member updated successfully", true);
                }
                else
                {
                    var existingMember = await _connection.AppGroupTeam
                        .FirstOrDefaultAsync(m => m.TeamId == addGroupMemberDto.TeamId && m.UserId == addGroupMemberDto.UserId);
                    if (existingMember != null)
                        return ("User is already a member of this team", false);
                }
                var teamMember = new AppGroupMember
                {
                    TeamId = addGroupMemberDto.TeamId,
                    UserId = addGroupMemberDto.UserId,
                    JoinedAt = DateTime.Now,
                    CreatedBy = UserId
                };

                await _connection.AppGroupTeam.AddAsync(teamMember);
                await _connection.SaveChangesAsync();

                return ("Team member added successfully", true);
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

                // STEP 1: Get all teams
                var teams = await _connection.AppTeam
                    .Where(x => x.IsActive == 1)
                    .ToListAsync();

                // STEP 2: Get all members with user info (join)
                var members = await (
                    from gm in _connection.AppGroupTeam
                    join u in _connection.AppUser
                        on gm.UserId equals u.UserId into userJoin
                    from u in userJoin.DefaultIfEmpty()   // LEFT JOIN

                    select new
                    {
                        gm.MemberId,
                        gm.TeamId,
                        gm.UserId,
                        UserName = u != null ? u.UserName : null,
                        UserImage = u != null ? u.ImageName : null
                    }
                ).ToListAsync();

                // STEP 3: Map result (ENSURE TEAM ALWAYS SHOWS)
                var result = teams.Select(team => new TeamDto
                {
                    TeamId = team.TeamId,
                    Name = team.Name,
                    ImageName = team.TeamImage,
                    CreatedBy = team.CreatedBy,
                    CreatedAt = team.CreatedAt,
                    IsActive = team.IsActive,


                    Members = members
                        .Where(m => m.TeamId == team.TeamId)
                        .Select(m => new TeamMemberDto
                        {
                            MemberId = m.MemberId,
                            TeamId = m.TeamId??0,
                            UserId = m.UserId,
                            UserName = m.UserName,
                            UserImage = m.UserImage
                        })
                        .ToList()
                })
                .ToList();


                return ("Team list retrieved successfully", true, result);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false, new List<TeamDto>());
            }
        }
    }
}
