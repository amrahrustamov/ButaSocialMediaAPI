using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using ButaAPI.Services.Concretes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ButaAPI.Controllers.Client
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthExceptions _authExceptions;
        private readonly ButaDbContext _butaDbContext;
        private readonly IMailkitEmailService _emailService;
        private readonly CookieService _cookieService;
        private readonly ModifyText _modifyText;
        public AuthenticationController(AuthExceptions authExceptions, ButaDbContext butaDbContext, IMailkitEmailService emailService, CookieService cookieService, ModifyText modifyText)
        {
            _authExceptions = authExceptions;
            _butaDbContext = butaDbContext;
            _emailService = emailService;
            _cookieService = cookieService;
            _modifyText = modifyText;
        }
        #region Send Email for Reset
        //[HttpPost]
        //[Route("auth/reset_password")]
        //public IActionResult ResetPassword([FromBody] string email)
        //{
        //    var checkingList = _authExceptions.CheckEmail(email);

        //    string emailExist = _butaDbContext.Users.FirstOrDefault(x => x.Email == email).ToString();
        //    if (emailExist != null)
        //    {
        //        _emailService.SendEmail("Reset password","<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Reset password</title>\r\n</head>\r\n<body>\r\n    <h2>Reset password</h2>\r\n    <form action=\"http://localhost:5065/authentication/auth/get_new_password\" method=\"post\">\r\n        <label for=\"password\">New password:</label><br>\r\n        <input type=\"password\" id=\"password\" name=\"password\" required><br><br>\r\n        <label for=\"password2\">Repeat password:</label><br>\r\n        <input type=\"password2\" id=\"password2\" name=\"password2\" required><br><br></br>\r\n\r\n        <input type=\"submit\" value=\"Şifre Sıfırlama Bağlantısı Gönder\">\r\n    </form>\r\n</body>\r\n</html>", emailExist);
        //        return Ok();
        //    }
        //    ModelState.AddModelError(checkingList.Key, checkingList.Content);
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }
        //    return NotFound();
        //}
        //[HttpPost]
        //[Route("auth/get_new_password")]
        //public IActionResult GetNewPassword([FromBody] string password, string password2)
        //{
        //    return Ok();
        //}

        #endregion

        #region Register

        [HttpPost]
        [Route("auth/register")]
        public IActionResult Register([FromBody] RegisterViewModel registerUserViewModel)
        {
            foreach (var item in _authExceptions.CheckUserAtRegister(registerUserViewModel))
            {
                if (null != item) { ModelState.AddModelError(item.Key, item.Content); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
            }
            User user = new User
            {
                FirstName = _modifyText.NormalizeName(registerUserViewModel.FirstName),
                LastName = _modifyText.NormalizeName(registerUserViewModel.LastName),
                Email = registerUserViewModel.Email,
                Password = registerUserViewModel.Password,
                PhoneNumber = registerUserViewModel.PhoneNumber,
                ProfileImage = "default.jpg"
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

            await _cookieService.CreateCookie(loginViewModel);

            var user = _butaDbContext.Users.FirstOrDefault(u => u.Email == loginViewModel.Email && u.Password == loginViewModel.Password);
            if(user != null) return Ok(user.Id);
            return NotFound(); 
        }
        #endregion

        #region Logout

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _cookieService.RemoveCookie();
            return Ok();
        }
        #endregion
    }
}