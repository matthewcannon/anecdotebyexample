using AnecdoteByExampleTwo.Application.CommandHandlers;
using AnecdoteByExampleTwo.Application.Commands;
using AnecdoteByExampleTwo.Application.Events;

namespace AnecdoteByExampleTwo.Application.Tasks
{
    public class ConfirmOrder : IHandle<PaymentRejected>
    {
        IHandleSendEmail _handleSendEmail;
        readonly HandleMakePayment _handleMakePayment;

        public ConfirmOrder(IHandleSendEmail handleSendEmail, HandleMakePayment handleMakePayment)
        {
            _handleSendEmail = handleSendEmail;
            _handleMakePayment = handleMakePayment;
        }

        public void Execute(Order order)
        {
            _handleMakePayment.Handle(new MakePayment(order.Payment));

            var orderConfirmationEmail = new OrderConfirmationEmail("noreply@confirm-order.com", order.Email);
            _handleSendEmail.Handle(new SendEmail(orderConfirmationEmail));
        }

        public void Handle(PaymentRejected @event)
        {
            _handleSendEmail = new NullHandleSendEmail();
        }
    }
}