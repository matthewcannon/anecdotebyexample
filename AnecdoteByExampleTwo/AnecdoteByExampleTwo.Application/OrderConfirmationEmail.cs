namespace AnecdoteByExampleTwo.Application
{
    public class OrderConfirmationEmail : Email
    {
        public OrderConfirmationEmail(string from, string to) : base(from, to) { }
    }
}