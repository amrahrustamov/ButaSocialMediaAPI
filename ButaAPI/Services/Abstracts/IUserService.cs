using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;

namespace ButaAPI.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsCurrentUserAuthenticated();
        public User GetCurrentUser();
        public UserPrivateInfo GetUserShortInfo(int id);
        public UserPrivateInfo GetUserInfo(int id);
    }
}
