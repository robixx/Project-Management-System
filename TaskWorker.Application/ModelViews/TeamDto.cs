using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class TeamDto
    {
        public int TeamId { get; set; }       
        public string? Name { get; set; }       
        public IFormFile? TeamImage { get; set; }
        public string? ImageName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int IsActive { get; set; }
        public List<TeamMemberDto>? Members { get; set; }
    }
}
