using ButaAPI.Database.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Message:BaseEntity<int>
    {
        public string Content { get; set; }
        public DateTime SendingTime { get; set; } = DateTime.Now;
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
