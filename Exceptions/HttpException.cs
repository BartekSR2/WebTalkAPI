using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebTalkApi.Exceptions
{
    public class HttpException: Exception
    {
        public virtual int StatusCode { get;  } = 500;
        public HttpException(string message):base(message)
        {
        } 


        
    }
}
