using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class FileShareDto
    {
        public int FileId { get; set; }
        public int UserId { get; set; }
        public int PermissionType { get; set; }
        public int? IsLocked { get; set; } = 0;
        public int Status { get; set; } = 1;
    }
}
