using ButaAPI.Database;
using ButaAPI.Database.CheckViewModel;
using ButaAPI.Database.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ButaAPI.Exceptions
{
    public class AuthExceptions
    {
        private readonly ButaDbContext _butaDbContext;
        public AuthExceptions(ButaDbContext butaDbContext)
        {
            _butaDbContext = butaDbContext;
        }

        #region LoginExceptions
        public ValidationResponseModel? CheckEmailAndPassword(string email, string password)
        {
            var result = _butaDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (result == null) return new ValidationResponseModel { Key = "Email", Content = "Wrong email! Try available email." };
            if (result.Password != password) return new ValidationResponseModel { Key = "Password", Content = "Wrong password!" };

            return null;
        }
        public ValidationResponseModel? CheckEmail(string email)
        {
            var result = _butaDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (result == null) return new ValidationResponseModel { Key = "Email", Content = "Wrong email! Try available email." };

            return null;
        }
        #endregion

        #region RegisterExceptions
        public List<ValidationResponseModel> CheckUserAtRegister(RegisterViewModel registerUserViewModel)
        {
            List<ValidationResponseModel> results = new List<ValidationResponseModel>();
            if (CheckUserFirstName(registerUserViewModel.FirstName) != null)
            {
                results.Add(CheckUserFirstName(registerUserViewModel.FirstName));
            }
            if (CheckUserLastName(registerUserViewModel.LastName) != null)
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

        public ValidationResponseModel? CheckUserFirstName(string firstName)
        {
            string pattern = @"^[a-zA-Z]{1,15}$";

            if (string.IsNullOrEmpty(firstName)) return new ValidationResponseModel { Key = "FirstName", Content = "First name is required." };
            if (!Regex.IsMatch(firstName, pattern)) return new ValidationResponseModel { Key = "FirstName", Content = "First name must be only letters and length between 1 and 15 characters" };

            return null;
        }
        public ValidationResponseModel? CheckUserLastName(string lastName)
        {
            string pattern = @"^[a-zA-Z]{1,18}$";

            if (string.IsNullOrEmpty(lastName)) return new ValidationResponseModel { Key = "LastName", Content = "Last name is required." };
            if (!Regex.IsMatch(lastName, pattern)) return new ValidationResponseModel { Key = "LastName", Content = "Last name must be only letters and length between 1 and 18 characters" };

            return null;
        }
        public ValidationResponseModel? CheckUserEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email)) return new ValidationResponseModel { Key = "Email", Content = "Email is required." };
            if (!Regex.IsMatch(email, pattern)) return new ValidationResponseModel { Key = "Email", Content = "The email format is invalid" };

            return null;
        }
        public ValidationResponseModel? CheckEmailExist(string email)
        {
            var result = _butaDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (result != null) return new ValidationResponseModel { Key = "Email", Content = "This email already exist!" };

            return null;
        }
        public ValidationResponseModel? CheckUserPassword(string password)
        {
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,14}$";

            if (string.IsNullOrEmpty(password)) return new ValidationResponseModel { Key = "Password", Content = "Password is required" };
            if (!Regex.IsMatch(password, pattern)) return new ValidationResponseModel { Key = "Password", Content = "Password must be at least one letter, one digit, one special character, and passowrd length between 8 and 14 characters" };

            return null;
        }

        #endregion
    }
}
