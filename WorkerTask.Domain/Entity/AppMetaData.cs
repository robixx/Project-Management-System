using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_MetaData")]
    public class AppMetaData
    {
        [Key]
        public int Id { get; set; } 

        public string? DataElementLevel { get; set; } 

        public int? IsActive { get; set; }
    }
}
