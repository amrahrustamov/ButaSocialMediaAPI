using ButaAPI.Database;
using ButaAPI.Database.CheckViewModel;
using ButaAPI.Database.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ButaAPI.Exceptions.Register
{
    public class RegisterExceptions
    {
        private readonly ButaDbContext _butaDbContext;
        public RegisterExceptions(ButaDbContext butaDbContext)
        {
            _butaDbContext = butaDbContext;
        }

        public List<RegisterValidationModel> CheckUserAtRegister(RegisterUserViewModel registerUserViewModel)
        {
            List<RegisterValidationModel> results = new List<RegisterValidationModel>();
            if(CheckUserFirstName(registerUserViewModel.FirstName) != null)
            {
                results.Add(CheckUserFirstName(registerUserViewModel.FirstName));
            }
            if(CheckUserLastName(registerUserViewModel.LastName) != null)
            {
                results.Add(CheckUserLastName(registerUserViewModel.LastName));
            }
            if (CheckUserEmail(registerUserViewModel.Email) != null)
            {
                results.Add(CheckUserEmail(registerUserViewModel.Email));
            }
            if (CheckEmailExist(registerUserViewModel.Email) != null)
            {
                results.Add(CheckEmailExist(registerUserViewModel.Email));
            }
            if (CheckUserPassword(registerUserViewModel.Password) != null)
            {
                results.Add(CheckUserPassword(registerUserViewModel.Password));
            }
            return results;
        }
        public RegisterValidationModel? CheckUserFirstName(string firstName)
        {
            string pattern = @"^[a-zA-Z]{1,15}$";

            if (string.IsNullOrEmpty(firstName)) return new RegisterValidationModel { Key = "FirstName", Content = "First name is required." };
            if (!Regex.IsMatch(firstName, pattern)) return new RegisterValidationModel { Key = "FirstName", Content = "First name must be only letters and length between 1 and 15 characters" };

            return null;
        }
        public RegisterValidationModel? CheckUserLastName(string lastName)
        {
            string pattern = @"^[a-zA-Z]{1,18}$";

            if (string.IsNullOrEmpty(lastName)) return new RegisterValidationModel { Key = "LastName", Content = "Last name is required." };
            if (!Regex.IsMatch(lastName, pattern)) return new RegisterValidationModel { Key = "LastName", Content = "Last name must be only letters and length between 1 and 18 characters" };

            return null;
        }
        public RegisterValidationModel? CheckUserEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email)) return new RegisterValidationModel { Key = "Email", Content = "Email is required." };
            if (!Regex.IsMatch(email, pattern)) return new RegisterValidationModel { Key = "Email", Content = "The email format is invalid" }; 

            return null;
        }
        public RegisterValidationModel? CheckEmailExist(string email)
        {
            var result = _butaDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (result != null) return new RegisterValidationModel { Key = "Email", Content = "This email already exist!" };

            return null;
        }
        public RegisterValidationModel? CheckUserPassword(string password)
        {
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,14}$";

            if (string.IsNullOrEmpty(password)) return new RegisterValidationModel { Key = "Password", Content = "Password is required" };
            if (!Regex.IsMatch(password, pattern)) return new RegisterValidationModel { Key = "Password", Content = "Password must be at least one letter, one digit, one special character, and passowrd length between 8 and 14 characters"};

            return null;
        }
    }
}
