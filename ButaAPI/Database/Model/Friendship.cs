using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ButaAPI.Database.Model
{
    public class Friendships
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FriendshipId { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
    }
}
