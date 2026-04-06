using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_WorkDocuments")]
    public class AppWorkDocument
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? DocType { get; set; } 

        public int? DocId { get; set; } 

        [MaxLength(250)]
        public string? FileName { get; set; }
        [MaxLength(250)]
        public string? FileType { get; set; }  

        public int? FileSize { get; set; } 

        public int? UploadedBy { get; set; } 

        public DateTime? UploadedAt { get; set; }

        public int Status { get; set; } = 1; 
    }
}
