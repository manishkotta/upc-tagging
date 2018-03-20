using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBusiness;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace UPCTaggingInterface.Diagnostics
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,ICommonService commonService)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                commonService.LogExceptionIntoDB(ex);
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
