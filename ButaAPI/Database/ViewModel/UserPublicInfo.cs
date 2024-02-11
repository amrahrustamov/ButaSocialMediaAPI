using ButaAPI.Database.Model.Enums;
using ButaAPI.Database.Model;

namespace ButaAPI.Database.ViewModel
{
    public class UserPublicInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Location? WhereFrom { get; set; }
        public Location? CurrentLocation { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender? Gender { get; set; }
        public List<string>? Activities { get; set; }
        public string? AboutUser { get; set; }
        public string? Work { get; set; }
        public List<string>? Education { get; set; }
        public RelationshipStatus? Relationship { get; set; }
        public string? ProfileImage { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Friendships>? Friendships { get; set; }
    }
}