using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_IssueReview")]
    public class AppIssueReview
    {
        public int Id { get; set; }
        public int RefId { get; set; }   
  // 1 = Project, 2 = Issue
        public int RefType { get; set; }      

        public int UserId { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int Status { get; set; }
    }
}
