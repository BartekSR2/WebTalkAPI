﻿namespace WebTalkApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next.Invoke(context);
            }
            catch(Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
