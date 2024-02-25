using ButaAPI.Database.Model;

namespace ButaAPI.Database.ViewModel
{
    public class GetCommentViewModel
    {
        public string Content { get; set; }
        public int CommentId { get; set; }
        public User Owner { get; set; }
        public DateTime DateTime { get; set; }
    }
}
