using Contracts;

using Microsoft.AspNetCore.Builder;

using System;

namespace CodeMazeApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
        }
    }
}
