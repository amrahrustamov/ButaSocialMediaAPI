using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ButaAPI.Database.Model
{
    public class FriendshipRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendsId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}