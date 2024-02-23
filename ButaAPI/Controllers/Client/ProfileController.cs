using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
        [Route("user_info")]
        public IActionResult GetUserInfo()
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
                UserSecure = user.UserSecure,
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

            user.FirstName = editProfileModel.FirstName;
            user.LastName = editProfileModel.LastName;
            user.Education = editProfileModel.Education;
            user.AboutUser = editProfileModel.AboutUser;
            user.Work = editProfileModel.Work;
            user.Activities = editProfileModel.Activities;
            user.Birthday = editProfileModel.Birthday;
            user.CurrentLocation = editProfileModel.CurrentLocation;
            user.Gender = editProfileModel.Gender;
            user.UserSecure = editProfileModel.UserSecure;
            user.Password = editProfileModel.Password;
            user.PhoneNumber = editProfileModel.PhoneNumber;
            user.Relationship = editProfileModel.Relationship;
            user.WhereFrom = editProfileModel.WhereFrom;

            _butaDbContext.SaveChanges();

            return Ok();
        }
        [HttpPost]
        [Route("add_profile_image")]
        public async Task<IActionResult> AddImage()
        {
            var files = Request.Form.Files;

            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            if (files != null)
            {
                foreach (var item in files)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    item.CopyTo(fileStream);

                    if (user.ProfileImage != null)
                    {
                        var oldFileName = user.ProfileImage.ToString();
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", oldFileName);
                        if(Directory.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    user.ProfileImage = fileName;
                    _butaDbContext.Update(user);
                    _butaDbContext.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("get_profile_image")]
        public async Task<IActionResult> GetProfileImage()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var currentUser = _userService.GetCurrentUser();
            var user = _butaDbContext.Users.FirstOrDefault(u => u.Email == currentUser.Email);
            if (user.ProfileImage != null)
            {
                 var image = Path.Combine("C:\\Users\\Amrah\\Desktop\\ButaSocialMediaAPI\\ButaAPI\\wwwroot\\Uploads\\Images", user.ProfileImage);
                 if (System.IO.File.Exists(image))
                 {
                     return File(System.IO.File.ReadAllBytes(image), "image/jpeg");
                 }
            }
            var defaultImage = Path.Combine("C:\\Users\\Amrah\\Desktop\\ButaSocialMediaAPI\\ButaAPI\\wwwroot\\Uploads\\Default" , user.ProfileImage);
            if (System.IO.File.Exists(defaultImage))
            {
                return File(System.IO.File.ReadAllBytes(defaultImage), "image/jpeg");
            }
            return NotFound();
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
        [HttpPost]
        [Route("user_blogs")]
        public IActionResult GetUserBlogs()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var userBlogs = _butaDbContext.Blogs.OrderByDescending(b => b).Where(ub => ub.OwnerId == user.Id).ToList();

            return Ok(userBlogs);
        }
    }
}