using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Infrastructure.Services;

namespace TaskWorker.Infrastructure.Utility
{
    public static class ServiceInjection
    {
        public static void InjectService(this IServiceCollection services)
        {
            //services.AddHttpContextAccessor();
            services.AddScoped<IAuth, AuthServices>();
            services.AddScoped<IBaseData, MetaService>();
            services.AddScoped<IUserInfo, UserInfoService>();
        }
    }
}
