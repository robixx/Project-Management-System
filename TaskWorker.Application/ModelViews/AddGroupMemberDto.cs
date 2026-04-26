using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class AddGroupMemberDto
    {
        public int MemberId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
}
