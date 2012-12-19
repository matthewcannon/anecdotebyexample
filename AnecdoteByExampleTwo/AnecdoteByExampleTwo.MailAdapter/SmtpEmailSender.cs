using System.Net.Mail;
using AnecdoteByExampleTwo.Application;

namespace AnecdoteByExampleTwo.MailAdapter
{
    public class SmtpEmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            var mailMessage = new MailMessage(email.From, email.To);

            var smtpClient = new SmtpClient();

            smtpClient.Send(mailMessage);
        }
    }
}
