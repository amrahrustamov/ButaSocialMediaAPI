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
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public User GetCurrentUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims
            .Select(c => c.Value)
            .ToList();
            return  _butaDbContext.Users.FirstOrDefault(user => user.Email == claims[0]);
        }

        public UserPrivateInfo GetUserShortInfo(int id)
        {
            var user = _butaDbContext.Users.FirstOrDefault(u => u.Id == id);

            UserPrivateInfo userPrivateInfo = new UserPrivateInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AboutUser = user.AboutUser,
                ProfileImage = user.ProfileImage,
            };

            return userPrivateInfo;
        }
        public UserPrivateInfo GetUserInfo(int id)
        {
            var user = _butaDbContext.Users.FirstOrDefault(u => u.Id == id);

            UserPrivateInfo userPrivateInfo = new UserPrivateInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AboutUser = user.AboutUser,
                ProfileImage = user.ProfileImage
            };

            return userPrivateInfo;
        }
    }
}