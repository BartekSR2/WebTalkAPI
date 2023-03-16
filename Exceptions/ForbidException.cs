namespace WebTalkApi.Exceptions
{
    public class ForbidException:HttpException
    {
        public override int StatusCode { get;  } = 403;
        public ForbidException(string message):base(message)
        {

        }
    }
}
