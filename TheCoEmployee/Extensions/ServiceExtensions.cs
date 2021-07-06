using Contracts;

using LoggerService;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Repository;

using System;

namespace CodeMazeApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsConfig", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        public static void AddLogger(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
