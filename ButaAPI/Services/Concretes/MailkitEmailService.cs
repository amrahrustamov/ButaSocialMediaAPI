using ButaAPI.Database.Model;
using ButaAPI.Services.Abstracts;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Xml.Linq;

namespace ButaAPI.Services.Concretes
{
    public class MailkitEmailService : IMailkitEmailService
    {
        public void SendEmail(string subject, string content, string receipent)
        {
            var messageDto = new MessageDto
            {
                Subject = subject,
                Content = content,
                Receipents = new List<string> { receipent }
            };
            SendEmail(messageDto);
        }
        public void SendEmail(string subject, string content, params string[] receipents)
        {
            var messageDto = new MessageDto
            {
                Subject = subject,
                Content = content,
                Receipents = receipents.ToList()
            };
            SendEmail(messageDto);
        }
        public void SendEmail(MessageDto messageDto)
        {
            var emailMessage = CreateEmailMessage(messageDto);
            AuthAndSend(emailMessage);
        }
        public MimeMessage CreateEmailMessage(MessageDto messageDto)
        {
            var emailMessage = new MimeMessage();
            emailMessage.Subject = messageDto.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = messageDto.Content };
            emailMessage.From.Add(new MailboxAddress("amrahrustamov94@yandex.com", "amrahrustamov94@yandex.com"));
            emailMessage.To.AddRange(messageDto.Receipents.Select(r => new MailboxAddress(r, r)));

            return emailMessage;
        }
        public void AuthAndSend(MimeMessage mimeMessage)
        {
           var client = new SmtpClient();

            try
            {
                client.Connect("smtp.yandex.com.tr", 465, true);
                client.Authenticate("amrahrustamov94@yandex.com", "fkrdxyhjaimrbvvh");
                client.Send(mimeMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
