namespace AnecdoteByExampleTwo.Application
{
    public class Order
    {
        public Payment Payment { get; private set; }
        public string Email { get; private set; }

        public Order(string email, Payment payment)
        {
            Email = email;
            Payment = payment;
        }
    }
}
