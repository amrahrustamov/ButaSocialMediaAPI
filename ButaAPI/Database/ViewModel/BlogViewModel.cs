using ButaAPI.Database.Model;

namespace ButaAPI.Database.ViewModel
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Location? Location { get; set; }
        public List<string>? Tags { get; set; }
        public ICollection<Comment>? Commets { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public string OwnerFullName { get; set; }
        public bool IsPublic { get; set; } = true;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public List<string>? Image { get; set; }
    }
}
