using AnecdoteByExampleTwo.Application;

namespace AnecdoteByExampleTwo.Factory
{
    public class TaskFactory
    {
        public ConfirmOrder ConfirmOrder(EventAggregator eventAggregator)
        {
            return new ConfirmOrder(new HandleSendEmail(eventAggregator));
        }
    }
}