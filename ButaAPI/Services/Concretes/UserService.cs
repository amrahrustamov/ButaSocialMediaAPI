using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Services.Abstracts;
using System.Security.Claims;

namespace ButaAPI.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ButaDbContext _butaDbContext;
        public UserService(IHttpContextAccessor httpContextAccessor, ButaDbContext butaDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _butaDbContext = butaDbContext;
        }

        public bool IsCurrentUserAuthenticated()
        {
            var user = _httpContextAccessor.HttpContext.Request.Cookies.ToList();
            if (user.Count > 0)
            {
                return true;
            }
            return false;
        }

        public User GetCurrentUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var claim = claims[0];
            var currentUser = _butaDbContext.Users.FirstOrDefault(u => u.Email == claim.Value);
            return currentUser;
        }
    }
}