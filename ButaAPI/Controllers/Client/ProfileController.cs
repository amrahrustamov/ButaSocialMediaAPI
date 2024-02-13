using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ButaAPI.Controllers.Client
{
    [Authorize]
    [Route("profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ButaDbContext _butaDbContext;

        public ProfileController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _butaDbContext = butaDbContext;
        }

        [HttpGet]
        [Route("get_user_info")]
        public IActionResult EditProfile()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var userinfo = _butaDbContext.Users.Where(u => u.Id == user.Id).ToList();

            EditProfileModel editProfileModel = new EditProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Education = user.Education,
                AboutUser = user.AboutUser,
                Work = user.Work,
                Activities = user.Activities,
                Birthday = user.Birthday,
                CurrentLocation = user.CurrentLocation,
                Gender = user.Gender,
                IsPrivate = user.IsPrivate,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage,
                Relationship = user.Relationship,
                WhereFrom = user.WhereFrom
            };

            return Ok(editProfileModel);
        }

        [HttpPost]
        [Route("edit_user_info")]
        public IActionResult EditProfile(EditProfileModel editProfileModel)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var userinfo = _butaDbContext.Users.Where(u => u.Id == user.Id).ToList();

            User userInfo = new User
            {
                FirstName = editProfileModel.FirstName,
                LastName = editProfileModel.LastName,
                Email = editProfileModel.Email,
                Education = editProfileModel.Education,
                AboutUser = editProfileModel.AboutUser,
                Work = editProfileModel.Work,
                Activities = editProfileModel.Activities,
                Birthday = editProfileModel.Birthday,
                CurrentLocation = editProfileModel.CurrentLocation,
                Gender = editProfileModel.Gender,
                IsPrivate = editProfileModel.IsPrivate,
                Password = editProfileModel.Password,
                PhoneNumber = editProfileModel.PhoneNumber,
                ProfileImage = editProfileModel.ProfileImage,
                Relationship = editProfileModel.Relationship,
                WhereFrom = editProfileModel.WhereFrom
            };

            _butaDbContext.Users.Update(userInfo);
            _butaDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("add_profile_image")]
        public async Task<IActionResult> AddImage(IFormFile image)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", fileName);

            using var fileStream = new FileStream(path, FileMode.Create);
            image.CopyTo(fileStream);

            user.ProfileImage = fileName;
            _butaDbContext.SaveChanges();

            return Ok();
        }
        [HttpGet]
        [Route("get_profile_image")]
        public async Task<IActionResult> GetProfileImage()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", user.ProfileImage);

            return Ok(image);
        }
        [HttpPost]
        [Route("update_profile_image")]
        public async Task<IActionResult> UpdateImage(IFormFile image)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var pathNew = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", fileName);
            var pathOld = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", user.ProfileImage);

            using var fileStream = new FileStream(pathNew, FileMode.Create);
            image.CopyTo(fileStream);

            user.ProfileImage = fileName;
            _butaDbContext.SaveChanges();
            System.IO.File.Delete(pathOld);

            return Ok();
        }
        [HttpPost]
        [Route("delete_profile_image")]
        public async Task<IActionResult> DeleteProfileImage()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", user.ProfileImage);

            user.ProfileImage = null;
            _butaDbContext.SaveChanges();
            System.IO.File.Delete(image);
            return Ok();
        }
    }
}