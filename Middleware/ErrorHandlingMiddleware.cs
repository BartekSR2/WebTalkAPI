using WebTalkApi.Exceptions;

namespace WebTalkApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException badRequest)
            {
                _logger.LogWarning(badRequest, badRequest.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch(Exception e)
            {
                _logger.LogWarning(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
