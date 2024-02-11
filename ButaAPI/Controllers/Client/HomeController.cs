using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ButaAPI.Controllers.Client
{
    [Authorize]
    [Route("homepage")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        ButaDbContext _butaDbContext;

        public HomeController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _butaDbContext = butaDbContext;
        }


        #region Current User Info
        [HttpGet]
        [Route("user")]
        public IActionResult UserInfo()
        {
            if(!_userService.IsCurrentUserAuthenticated())
            {
                return NotFound();
            }
            var item = _userService.GetCurrentUser();

            return Ok();
        }
        #endregion

        #region All Post
        [HttpGet]
        [Route("blogs")]
        public IActionResult GetBlogs()
        {
            if (!_userService.IsCurrentUserAuthenticated())
            {
                return NotFound();
            }
            var user = _userService.GetCurrentUser();

            return Ok();
        }

        [HttpPost]
        [Route("add_blog")]
        public IActionResult AddBlog([FromBody] AddBlogViewModel addBlogViewModel)
        {
            var user = _userService.GetCurrentUser();

            if(addBlogViewModel.Image != null || addBlogViewModel.Content != null)
            {
                Blog blog = new Blog
                {
                   OwnerId = user.Id,
                   Content = addBlogViewModel.Content,
                   Location = addBlogViewModel.Location,
                   Tags = addBlogViewModel.Tags,
                   Image = addBlogViewModel.Image,
                   DateTime = addBlogViewModel.DateTime
                };
                _butaDbContext.Add(blog);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            if (null == addBlogViewModel.Image && null == addBlogViewModel.Content) { ModelState.AddModelError("Empty", "Content or Image can not be empty"); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return BadRequest();
        }
        #endregion
    }
}