using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_EncriptedData")]
    public class AppEncryptedData
    {
        [Key]
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        [StringLength(250)]
        public string? EncriptedData { get; set; }
    }
}
