using ButaAPI.Database;
using ButaAPI.Database.CheckViewModel;
using System.Text.RegularExpressions;

namespace ButaAPI.Exceptions
{
    public class PostExceptions
    {
        private readonly ButaDbContext _butaDbContext;
        public PostExceptions(ButaDbContext butaDbContext)
        {
            _butaDbContext = butaDbContext;
        }


        public ValidationResponseModel? CheckUserFirstName(string firstName)
        {
            string pattern = @"^[a-zA-Z]{1,15}$";

            if (string.IsNullOrEmpty(firstName)) return new ValidationResponseModel { Key = "FirstName", Content = "First name is required." };
            if (!Regex.IsMatch(firstName, pattern)) return new ValidationResponseModel { Key = "FirstName", Content = "First name must be only letters and length between 1 and 15 characters" };

            return null;
        }
    }
}
