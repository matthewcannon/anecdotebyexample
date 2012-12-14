namespace AnecdoteByExampleTwo.Application
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

        public void Handle<T>(SendEmail<T> sendEmail) where T : Email
        {
            _emailSender.SendEmail(sendEmail.Email);
            _eventAggregator.Publish(new EmailSent<T>());
        }
    }
}
