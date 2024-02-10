using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using ButaAPI.Services.Concretes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ButaAPI.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthExceptions _authExceptions;
        private readonly ButaDbContext _butaDbContext;
        private readonly IUserService _userService;
        public AuthenticationController(AuthExceptions authExceptions, ButaDbContext butaDbContext, UserServices userService)
        {
            _authExceptions = authExceptions;
            _butaDbContext = butaDbContext;
            _userService = userService;
        }

        #region Register

        [HttpPost]
        [Route("auth/register")]
        public IActionResult Register([FromBody] RegisterViewModel registerUserViewModel)
        {
            var checkingList = _authExceptions.CheckUserAtRegister(registerUserViewModel);

            foreach (var item in checkingList)
            {
                if (null != item) { ModelState.AddModelError(item.Key, item.Content); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
            }

            User user = new User
            {
                FirstName = registerUserViewModel.FirstName.ToLower(),
                LastName = registerUserViewModel.LastName.ToLower(),
                Email = registerUserViewModel.Email,
                Password = registerUserViewModel.Password,
                CreateTime = DateTime.UtcNow,
            };
            _butaDbContext.Add(user);
            _butaDbContext.SaveChanges();
            return Ok();
        }
        #endregion

        #region Login

        [HttpPost]
        [Route("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var item = _authExceptions.CheckEmailAndPassword(loginViewModel.Email, loginViewModel.Password);

                if (null != item) { ModelState.AddModelError(item.Key, item.Content); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                var claims = new List<Claim>
                {
                    new Claim("Current_User", loginViewModel.Email)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

            return Ok();
        }
        #endregion

        #region Logout

        [HttpGet]
        [Route("auth/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        #endregion
    }
}
