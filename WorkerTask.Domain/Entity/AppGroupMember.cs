using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_GroupMember")]
    public class AppGroupMember
    {
        [Key] 
        public int MemberId { get; set; }   
        public int UserId { get; set; }
        public int? TeamId { get; set; }      
        public DateTime? JoinedAt { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
