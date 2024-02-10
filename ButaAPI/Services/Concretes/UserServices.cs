using ButaAPI.Services.Abstracts;

namespace ButaAPI.Services.Concretes
{
    public class UserServices : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsCurrentUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
