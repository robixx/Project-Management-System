using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Domain.Entity
{
    [Table("app_Department_Approved")]
    public class AppDepartmentApproved
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int DepartmentId { get; set; }
    }
}
