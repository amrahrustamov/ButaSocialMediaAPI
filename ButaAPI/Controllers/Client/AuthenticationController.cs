using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ButaAPI.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly RegisterExceptions _registerExceptions;
        public AuthenticationController(RegisterExceptions registerExceptions)
        {
            _registerExceptions = registerExceptions;
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
                CreateTime = DateTime.Now,
            };
            return Ok();
        }
        #endregion


        //[HttpPost]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
