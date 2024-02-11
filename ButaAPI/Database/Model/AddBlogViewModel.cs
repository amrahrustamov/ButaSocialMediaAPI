namespace ButaAPI.Database.Model
{
    public class AddBlogViewModel
    {
        public string? Content { get; set; }
        public Location? Location { get; set; }
        public List<string>? Tags { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? Image { get; set; }
    }
}
