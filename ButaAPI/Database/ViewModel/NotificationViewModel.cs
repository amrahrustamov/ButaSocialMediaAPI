using ButaAPI.Database.Model;

namespace ButaAPI.Database.ViewModel
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public User Receiver { get; set; }
        public User Sender { get; set; }
        public DateTime DateTime { get; set; }
        public bool Read { get; set; }
        public string Content { get; set; }
        public string URl { get; set; }
    }
}
