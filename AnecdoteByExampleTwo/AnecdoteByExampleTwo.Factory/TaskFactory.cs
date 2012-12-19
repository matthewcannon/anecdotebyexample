using AnecdoteByExampleTwo.Application;
using AnecdoteByExampleTwo.Application.CommandHandlers;
using AnecdoteByExampleTwo.Application.Tasks;

namespace AnecdoteByExampleTwo.Factory
{
    public class TaskFactory
    {
        public ConfirmOrder ConfirmOrder(
            EventAggregator eventAggregator,
            IEmailSender emailSender,
            IPaymentHandler paymentHandler)
        {
            return new ConfirmOrder(new HandleSendEmail(eventAggregator, emailSender), new HandleMakePayment(eventAggregator, paymentHandler));
        }
    }
}