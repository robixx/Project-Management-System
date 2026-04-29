using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class AssignTypeDto
    {
        public int Id { get; set; }

        public string? TypeName { get; set; }

        public int IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
