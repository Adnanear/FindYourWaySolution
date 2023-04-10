using Azure.Core;
using FindYourWay.Controllers;
using FindYourWay.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace FindYourWay.Middlewares
{

    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            bool isAuthPath = httpContext.Request.Path.ToString().StartsWith("/api/auth");
            if( isAuthPath )
            {
                await _next(httpContext);
                return;
            }

            var hasAuthorizationHeader = httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken);

            var _context = httpContext.RequestServices.GetRequiredService<AppDbContext>();
            var accounts = await _context.Accounts.ToListAsync();

            var targetAccount = accounts.FirstOrDefault(x => x.AccessToken == authorizationToken.ToString());

            if ( !hasAuthorizationHeader || targetAccount is null )
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
