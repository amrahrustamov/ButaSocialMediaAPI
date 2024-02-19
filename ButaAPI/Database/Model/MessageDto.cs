namespace ButaAPI.Database.Model
{
    public class MessageDto
    {

        public MessageDto() { }


        public MessageDto(string subject, string content, List<string> receipents)
        {
            Subject = subject;
            Content = content;
            Receipents = receipents;
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> Receipents { get; set; }
    }
}
