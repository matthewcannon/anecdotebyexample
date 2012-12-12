namespace AnecdoteByExampleTwo.Application
{
    public interface IPaymentHandler
    {
        PaymentStatus Handle(Payment payment);
    }
}