namespace AnecdoteByExampleTwo.Application
{
    public class ConfirmOrder
    {
        readonly HandleSendEmail _handleSendEmail;
        readonly HandleMakePayment _handleMakePayment;

        public ConfirmOrder(HandleSendEmail handleSendEmail, HandleMakePayment handleMakePayment)
        {
            _handleSendEmail = handleSendEmail;
            _handleMakePayment = handleMakePayment;
        }

        public void Execute(Order order)
        {
            _handleMakePayment.Handle(new MakePayment(order.Payment));

            var orderConfirmationEmail = new OrderConfirmationEmail("noreply@confirm-order.com", order.Email);
            _handleSendEmail.Handle(new SendEmail<OrderConfirmationEmail>(orderConfirmationEmail));
        }
    }
}