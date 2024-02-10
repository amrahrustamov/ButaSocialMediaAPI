using ButaAPI.Database.Model;

namespace ButaAPI.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsCurrentUserAuthenticated();
        public User GetCurrentUser();
    }
}
