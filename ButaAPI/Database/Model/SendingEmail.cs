using MimeKit;

namespace ButaAPI.Database.Model
{
    public class SendingEmail
    {
        public SendingEmail(string subject, string content, string receipent)
        {
            Subject = subject;
            Content = content;
            Receipent = receipent;
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public string Receipent {  get; set; }
    }
}
