using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ButaAPI.Database.Model
{
    public class FriendshipRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
