namespace AnecdoteByExampleTwo.Application
{
    public class ConfirmOrder
    {
        readonly ExecuteSendEmail _executeSendEmail;

        public ConfirmOrder(ExecuteSendEmail executeSendEmail)
        {
            _executeSendEmail = executeSendEmail;
        }

        public void Execute(Order order)
        {
            /*
             * do payment
             *
             */

            /*
             * 
             * dispatch order, etc.
             * 
             */

            var orderConfirmationEmail = new OrderConfirmationEmail("noreply@confirm-order.com", order.Email);
            _executeSendEmail.Execute(new SendEmail<OrderConfirmationEmail>(orderConfirmationEmail));
        }
    }
}