using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Utility
{
    public static class ServiceInjection
    {
        public static void InjectService(this IServiceCollection services)
        {
            //services.AddHttpContextAccessor();
            //services.AddScoped<IMenuSet, MenuDAO>();
        }
    }
}
