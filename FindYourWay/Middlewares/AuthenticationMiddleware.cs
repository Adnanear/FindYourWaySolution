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
            // Assign the next middleware request delegate
            _next = next;
        }

        // This is the tasks that gets invoked when a request hits the middleware
        public async Task Invoke(HttpContext httpContext)
        {
            // First let's check that the request validates `/api/auth/**` paths
            bool isAuthPath = httpContext.Request.Path.ToString().StartsWith("/api/auth");
            if( isAuthPath )
            {
                // If it is a auth paths we just need to ignore all the next verifications
                await _next(httpContext);
                return;
            }

            // If the request is an actual api call that's not `auth`

            // we first need to check if the request headers contains `Authorization` entry
            // and store the value of it within `authorizationToken` variable, defined at `out var authorizationToken`
            var hasAuthorizationHeader = httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken);

            // Let's get the database context from the request services
            var _context = httpContext.RequestServices.GetRequiredService<AppDbContext>();

            // List the available accounts from repository
            var accounts = await _context.Accounts.ToListAsync();

            // Try to find an account with the authorization token
            var targetAccount = accounts.FirstOrDefault(x => x.AccessToken == authorizationToken.ToString());

            if ( !hasAuthorizationHeader || targetAccount is null )
            {
                // If we failed to find any account with the provided token
                // we throw `401` unauthorized
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }

            // If the token was validated and everything was good
            // we can then pass the request to the next middleware or controller
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
