using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{

    [Table("app_FileShare")]
    public class AppFileShare
    {
        [Key]
        public int Id { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public int PermissionType { get; set; }       
        public int? IsLocked { get; set; }
        public int? Sharedby { get; set; }
        public DateTime? Sharedat { get; set; }
        public int Status { get; set; } = 1;
    }
}
