using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_FileUploads")]
    public class AppFileUpload
    {
        public int Id { get; set; }

        public int RefId { get; set; }

        public int RefType { get; set; }

        public string? FileName { get; set; }      

        public string? FileType { get; set; }

        public int UploadedBy { get; set; }

        public DateTime UploadedAt { get; set; }

        public int Status { get; set; }
    }
}
