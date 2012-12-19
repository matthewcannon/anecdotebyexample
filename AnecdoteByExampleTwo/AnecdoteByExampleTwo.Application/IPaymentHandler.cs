namespace AnecdoteByExampleTwo.Application
{
    public interface IPaymentHandler
    {   
        PaymentReceipt Handle(Payment payment);
    }
}