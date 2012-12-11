namespace AnecdoteByExampleTwo.Domain
{
    public class ConfirmOrder
    {
        readonly HandleSendEmail _handleSendEmail;

        public ConfirmOrder(HandleSendEmail handleSendEmail)
        {
            _handleSendEmail = handleSendEmail;
        }

        public void Execute()
        {
            _handleSendEmail.Handle(new SendEmail<OrderConfirmationEmail>());
        }
    }
}