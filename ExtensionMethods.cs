using System;
using Microsoft.Extensions.DependencyInjection;
using weBelieveIT.services;

namespace weBelieveIT
{
    public static class ExtensionMethods
    {
        public static void GetExtensionMethods(this IServiceCollection services)
        {
            services.AddTransient<UserServices>();
            services.AddTransient<JobService>();
            services.AddTransient<TeamService>();
            services.AddTransient<TaskService>();
        }
    }
}