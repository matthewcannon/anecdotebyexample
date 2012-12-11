namespace AnecdoteByExampleTwo.Domain
{
    public class HandleSendEmail
    {
        readonly EventAggregator _eventAggregator;

        public HandleSendEmail(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle<T>(SendEmail<T> sendEmail) where T : Email
        {
            _eventAggregator.Publish(new EmailSent<T>());
        }
    }
}
