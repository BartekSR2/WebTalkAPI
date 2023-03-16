using AutoMapper.Internal;
using WebTalkApi.Exceptions;

namespace WebTalkApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {

        private readonly IExceptionHandler _exceptionHandler;
        public ErrorHandlingMiddleware(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException badRequest)
            {
                await _exceptionHandler.HandleExceptionAsync(context, badRequest);
            }
            catch(NotFoundException notFound)
            {
                await _exceptionHandler.HandleExceptionAsync(context, notFound);
            }
            catch(ForbidException forbid)
            {
                await _exceptionHandler.HandleExceptionAsync(context, forbid);
            }
            catch(Exception e)
            {
                await _exceptionHandler.HandleExceptionAsync(context, new HttpException("Something went wrong"));
            }
        }

        

        
    }
}
