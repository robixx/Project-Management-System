using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        [MaxLength(50)]
        public string? RoleName { get; set; }

        public int? IsActive { get; set; }
    }
}
