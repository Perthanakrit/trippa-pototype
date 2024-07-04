using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Extension
{
    public static class MiddlerwareExtension
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyMiddleware>();
            // app.UseMiddleware<ExceptionMiddleware>();
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}