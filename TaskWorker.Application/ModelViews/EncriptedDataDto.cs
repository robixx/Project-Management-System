using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class EncriptedDataDto
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        [StringLength(250)]
        public string? EncriptedData { get; set; }
    }
}
