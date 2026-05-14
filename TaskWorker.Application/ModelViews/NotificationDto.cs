using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? MessageValue { get; set; }
        public int FromUser { get; set; }
        public int Isread { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }
    }
}
