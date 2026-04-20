using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class MenuPermissionDto
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int IsActive { get; set; }
    }
}
