using AnecdoteByExampleTwo.Application;
using AnecdoteByExampleTwo.Application.CommandHandlers;
using AnecdoteByExampleTwo.Application.Tasks;
using AnecdoteByExampleTwo.MailAdapter;
using AnecdoteByExampleTwo.ServiceAdapter;

namespace AnecdoteByExampleTwo.Factory
{
    public class TaskFactory
    {
        public ConfirmOrder ConfirmOrder()
        {
            return ConfirmOrder(
                new EventAggregator(),
                new SmtpEmailSender(),
                new ServicePaymentHandler());
        }

        public ConfirmOrder ConfirmOrder(
            EventAggregator eventAggregator,
            IEmailSender emailSender,
            IPaymentHandler paymentHandler)
        {
            var confirmOrder = new ConfirmOrder(
                new HandleSendEmail(eventAggregator, emailSender),
                new HandleMakePayment(eventAggregator, paymentHandler));
            
            eventAggregator.Register(confirmOrder);
            
            return confirmOrder;
        }
    }
}