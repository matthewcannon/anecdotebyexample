namespace AnecdoteByExampleTwo.Application
{
    public class ExecuteSendEmail
    {
        readonly EventAggregator _eventAggregator;
        readonly IEmailSender _emailSender;

        public ExecuteSendEmail(EventAggregator eventAggregator, IEmailSender emailSender)
        {
            _eventAggregator = eventAggregator;
            _emailSender = emailSender;
        }

        public void Execute<T>(SendEmail<T> sendEmail) where T : Email
        {
            _emailSender.SendEmail(sendEmail.Email);
            _eventAggregator.Publish(new EmailSent<T>());
        }
    }
}
