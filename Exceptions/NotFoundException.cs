namespace WebTalkApi.Exceptions
{
    public class NotFoundException:HttpException
    {
        public override int  StatusCode { get;  } = 404;
        public NotFoundException(string message): base(message)
        {

        }
    }
}
