namespace AnecdoteByExampleTwo.Application
{
    public class Order
    {
        public string Email { get; private set; }

        public Order(string email)
        {
            Email = email;
        }
    }
}
