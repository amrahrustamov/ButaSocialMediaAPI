using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class FriendshipRequest: BaseEntity<int>
    {
        public int UserId { get; set; }
        public int FriendsId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}