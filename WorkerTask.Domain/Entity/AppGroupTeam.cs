using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_GroupTeam")]
    public class AppGroupTeam
    {
        [Key] 
        public int MemberId { get; set; }       
        public string? ImageName { get; set; }       
        public string? TeamName { get; set; }        
        public int? TeamId { get; set; }      
        public DateTime? JoinedAt { get; set; }
    }
}
