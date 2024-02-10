using ButaAPI.Database;
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
        ButaDbContext _dbContext;

        public HomeController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _dbContext = butaDbContext;
        }

        [HttpGet]
        [Route("user")]
        public IActionResult UserInfo()
        {
            if(!_userService.IsCurrentUserAuthenticated())
            {
                return NotFound(); //fake
            }
            var item = _userService.GetCurrentUser();

            var posts = _dbContext.Blogs.ToList();



            return Ok();
        }

    }
}
