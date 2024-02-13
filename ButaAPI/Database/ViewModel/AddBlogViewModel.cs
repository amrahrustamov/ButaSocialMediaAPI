using ButaAPI.Database.Model;

namespace ButaAPI.Database.ViewModel
{
    public class AddBlogViewModel
    {
        public string? Content { get; set; }
        public Location? Location { get; set; }
        public List<string>? Tags { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public List<IFormFile>? Image { get; set; }
    }
}
