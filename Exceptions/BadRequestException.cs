namespace WebTalkApi.Exceptions
{
    public class BadRequestException:HttpException
    {
        public override int StatusCode { get;  } = 400;
        public BadRequestException(string message):base(message)
        {

        }
    }
}
