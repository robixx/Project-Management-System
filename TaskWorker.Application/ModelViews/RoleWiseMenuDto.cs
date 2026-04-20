using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class RoleWiseMenuDto
    {
        public int Id { get; set; }
        public string? MenuTitle { get; set; }
        public string? Urls { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int IsParent { get; set; }
        public string? Icon { get; set; }
        public int IsActive { get; set; }
        public string? PageArea { get; set; }
        public int IsSelected { get; set; } // 🔥 1 or 0
    }
}
