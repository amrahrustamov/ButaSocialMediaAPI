using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Model.Enums;

namespace ButaAPI.Database.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public Location? WhereFrom { get; set; }
        public Location? CurrentLocation { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender? Gender { get; set; }
        public List<string>? Activities { get; set; }
        public string? AboutUser { get; set; }
        public string? Work { get; set; }
        public List<string>? Education { get; set; }
        public UserStatus RegisterStatus { get; set; } = UserStatus.Waiting;
        public bool IsAdmin { get; set; } = false;
        public bool? IsPrivate { get; set; } = false;
        public RelationshipStatus? Relationship { get; set; }
        public string ProfileImage { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Friendships>? Friendships { get; set; }
        public ICollection<Notifications>? Notifications { get; set; }
        public ICollection<FriendshipRequest>? FriendshipRequests { get; set; }
    }
}
