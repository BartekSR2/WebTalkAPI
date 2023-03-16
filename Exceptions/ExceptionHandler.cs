

namespace WebTalkApi.Exceptions
{
    public interface IExceptionHandler
    {
        public Task HandleExceptionAsync(HttpContext context, HttpException exception);
    }
    public class ExceptionHandler:IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler( ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async Task HandleExceptionAsync(HttpContext context, HttpException exception)
        {
            _logger.LogWarning(exception, exception.Message);
            context.Response.StatusCode = exception.StatusCode;
            await context.Response.WriteAsync(exception.Message);
        }
    }
}
