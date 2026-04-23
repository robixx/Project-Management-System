using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class AppSecUserDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        [StringLength(250)]
        public string? LoginName { get; set; }
        [StringLength(250)]
        public string? HashPassword { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? IsActive { get; set; }
        
    }
}
