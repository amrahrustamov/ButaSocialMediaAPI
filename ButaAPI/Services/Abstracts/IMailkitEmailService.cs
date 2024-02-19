using ButaAPI.Database.Model;
using MimeKit;

namespace ButaAPI.Services.Abstracts
{
    public interface IMailkitEmailService
    {
       public void SendEmail(string subject, string content, string receipent);
       public void SendEmail(string subject, string content, params string[] receipents);
       public void SendEmail(MessageDto messageDto);
    }
}
