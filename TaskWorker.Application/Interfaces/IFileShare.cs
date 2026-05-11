using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface IFileShare
    {
        Task<(string Message, bool Status)> ShareFileAsync(FileShareDto fileshare);

        Task<(string Message, bool Status, List<CalendarDto> Data)> GetCalendarDataAsync(string StartDate, string ToDate);
    }
}
