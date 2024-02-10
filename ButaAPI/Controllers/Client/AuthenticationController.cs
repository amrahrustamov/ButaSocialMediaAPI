using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ButaAPI.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly RegisterExceptions _registerExceptions;
        private readonly ButaDbContext _butaDbContext;
        public AuthenticationController(RegisterExceptions registerExceptions, ButaDbContext butaDbContext)
        {
            _registerExceptions = registerExceptions;
            _butaDbContext = butaDbContext;
        }
        #region Register

        [HttpGet]
        [Route("auth/register")]
        public string Get(int id)
        {
            //burda cookie yoxlanis olacaq
            return "value";
        }

        [HttpPost]
        [Route("auth/register")]
        public IActionResult Post([FromBody] RegisterUserViewModel registerUserViewModel)
        {
            var checkingList = _registerExceptions.CheckUserAtRegister(registerUserViewModel);

            foreach (var item in checkingList)
            {
                if (null != item) { ModelState.AddModelError(item.Key, item.Content); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
            }

            User user = new User
            {
                FirstName = registerUserViewModel.FirstName,
                LastName = registerUserViewModel.LastName,
                Email = registerUserViewModel.Email,
                Password = registerUserViewModel.Password,
                CreateTime = DateTime.UtcNow,
            };
            _butaDbContext.Add(user);
            _butaDbContext.SaveChanges();
            return Ok();
        }
        #endregion
    }
}
