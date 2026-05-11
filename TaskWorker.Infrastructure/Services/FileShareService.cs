using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;
using TaskWorker.Infrastructure.DBConnection;

namespace TaskWorker.Infrastructure.Services
{
    public class FileShareService : IFileShare
    {

        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public FileShareService(DatabaseConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            _connection = connection;
            _httpcontextaccessor = httpContextAccessor;
        }

      

        public async Task<(string Message, bool Status)> ShareFileAsync(FileShareDto fileshare)
        {
            try
            {
                if(fileshare == null)
                {
                    return("File  Data is null", false);
                }

                var userId = _httpcontextaccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

                int sharedBy = int.TryParse(userId, out int uid) ? uid : 0;

                // 🔹 Check duplicate
                var exists = await _connection.AppFileShare
                    .FirstOrDefaultAsync(x =>
                        x.FileId == fileshare.FileId &&
                        x.UserId == fileshare.UserId);

                if (exists != null)
                    return ("User already shared this file", false);

                // 🔹 Create entity
                var entity = new AppFileShare
                {
                    FileId = fileshare.FileId,
                    UserId = fileshare.UserId,
                    PermissionType = fileshare.PermissionType,
                    IsLocked = fileshare.IsLocked,
                    Sharedby = sharedBy,
                    Sharedat = DateTime.Now,
                    Status = fileshare.Status
                };

                // 🔹 Save
                await _connection.AppFileShare.AddAsync(entity);
                await _connection.SaveChangesAsync();

                return ("File shared successfully", true);



            }
            catch(Exception ex)
            {
                return($"Error: {ex.Message}", false);
            }
            
        }


        public async Task<(string Message, bool Status, List<CalendarDto> Data)> GetCalendarDataAsync(string StartDate, string ToDate)
        {
            try
            {
                string[] formats = new[]
                             {
                                "dd-MM-yyyy",
                                "dd/MM/yyyy",
                                "yyyy-MM-dd"
                            };

                DateTime start = DateTime.ParseExact(
                    StartDate,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                );

                DateTime end = DateTime.ParseExact(
                    ToDate,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                );

                var data = await _connection.Set<CalendarDto>()
                    .FromSqlRaw(@"SELECT * FROM public.""fn_GetCalendarData""({0}::date, {1}::date)",start,end)
                    .ToListAsync();


                return ("Data Retrived Successfully", true, data);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false, new List<CalendarDto>());
            }
        }
    }
}
