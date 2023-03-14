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
            catch(NotFoundException notFound)
            {
                _logger.LogWarning(notFound, notFound.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch(ForbidException forbid)
            {
                _logger.LogWarning(forbid, forbid.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbid.Message);
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
