using ButaAPI.Database;
using ButaAPI.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ButaAPI.Controllers.Client
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ButaDbContext _butaDbContext;

        public UserController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _butaDbContext = butaDbContext;
        }
        #region Current User Info
        [HttpGet]
        [Route("get_all_Info")]
        public IActionResult UserAllInfo()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var item = _userService.GetCurrentUser();

            var userInfo = _butaDbContext.Users.Where(u => u.Email == item.Email).ToList();

            return Ok(userInfo);
        }
        #endregion
    }
}
