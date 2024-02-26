using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Services.Abstracts;

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

        public string AddNewImage(IFormFile item)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", fileName);
            using var fileStream = new FileStream(path, FileMode.Create);
            item.CopyTo(fileStream);
            return fileName;
        }
        public void RemoveOldImage(User user)
        {
            var oldFileName = user.ProfileImage.ToString();
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", oldFileName);
            if (File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
        }
    }
}