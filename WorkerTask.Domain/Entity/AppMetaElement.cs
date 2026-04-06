using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_MetaElement")]
    public class AppMetaElement
    {
        [Key]
        public int ElementId { get; set; } 

        public int? MetaDataId { get; set; } 

        public string? ElementValue { get; set; } 
    }
}
