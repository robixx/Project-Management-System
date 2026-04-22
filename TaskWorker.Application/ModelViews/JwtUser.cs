using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class JwtUser
    {
        public string? JWTId { get; set; }
        public int UserId { get; set; }     
        public string? DispalyName { get; set; }
        public string? RoleName { get; set; }
        public int RoleId { get; set; }
        public DateTime? TokenExpired { get; set; }
    }
}
