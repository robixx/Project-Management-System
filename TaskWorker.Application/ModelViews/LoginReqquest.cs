using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Application.ModelViews
{
    public class LoginReqquest
    {
        public string loginName { get; set; } = string.Empty;   
        public string password { get; set; } = string.Empty;   
    }
}
