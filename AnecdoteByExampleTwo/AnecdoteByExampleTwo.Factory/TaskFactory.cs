using AnecdoteByExampleTwo.Application;
using AnecdoteByExampleTwo.MailAdapters;

namespace AnecdoteByExampleTwo.Factory
{
    public class TaskFactory
    {
        public ConfirmOrder ConfirmOrder(EventAggregator eventAggregator)
        {
            return new ConfirmOrder(new ExecuteSendEmail(eventAggregator, new SmtpEmailSender()));
        }
    }
}