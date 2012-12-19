using AnecdoteByExampleTwo.Application.Commands;
using AnecdoteByExampleTwo.Application.Events;

namespace AnecdoteByExampleTwo.Application.CommandHandlers
{
    public class HandleSendEmail
    {
        readonly EventAggregator _eventAggregator;
        readonly IEmailSender _emailSender;

        public HandleSendEmail(EventAggregator eventAggregator, IEmailSender emailSender)
        {
            _eventAggregator = eventAggregator;
            _emailSender = emailSender;
        }

        public void Handle(SendEmail sendEmail)
        {
            _emailSender.SendEmail(sendEmail.Email);
            _eventAggregator.Publish(new EmailSent());
        }
    }
}
