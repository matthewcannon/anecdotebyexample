namespace AnecdoteByExampleTwo.Application
{
    public class ConfirmOrder
    {
        readonly ExecuteSendEmail _executeSendEmail;
        readonly ExecuteMakePayment _executeMakePayment;

        public ConfirmOrder(ExecuteSendEmail executeSendEmail, ExecuteMakePayment executeMakePayment)
        {
            _executeSendEmail = executeSendEmail;
            _executeMakePayment = executeMakePayment;
        }

        public void Execute(Order order)
        {
            _executeMakePayment.Execute(new MakePayment(order.Payment));

            var orderConfirmationEmail = new OrderConfirmationEmail("noreply@confirm-order.com", order.Email);
            _executeSendEmail.Execute(new SendEmail<OrderConfirmationEmail>(orderConfirmationEmail));
        }
    }
}