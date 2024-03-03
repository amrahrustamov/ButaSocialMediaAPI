using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Notifications: BaseEntity<int>,IAuditable
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Read { get; set; } = false;
        public string Content { get; set; }
        public string? URl {  get; set; }
    }
}
