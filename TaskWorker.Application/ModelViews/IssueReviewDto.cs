using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class IssueReviewDto
    {
        public int RefId { get; set; }
        public UploadRefType RefType { get; set; }
        public string? Comment { get; set; }
    }
}
