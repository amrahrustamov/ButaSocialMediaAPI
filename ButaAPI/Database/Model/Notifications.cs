using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Notifications: BaseEntity<int>
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool Read { get; set; } = false;
        public string Content { get; set; }
        public string? URl {  get; set; }
    }
}
