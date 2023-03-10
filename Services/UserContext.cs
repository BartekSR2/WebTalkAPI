using System.Security.Claims;

namespace WebTalkApi.Services
{
    public interface IUserContext
    {
        public ClaimsPrincipal User { get; }
        public int? UserId { get; }
    }
    public class UserContext:IUserContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public ClaimsPrincipal User
        {
            get { return _httpContext.HttpContext?.User;  }
        }

        public int? UserId
        {
            get { return User is null ? null 
                    : (int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)); }
        }
    }
}
