using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class DataElementDto
    {
        public int ElementId { get; set; }
        public int? MetaDataId { get; set; }
        public string? ElementValue { get; set; }
        public string? DataElementLevel { get; set; }
        public int? IsActive { get; set; }
    }
}
