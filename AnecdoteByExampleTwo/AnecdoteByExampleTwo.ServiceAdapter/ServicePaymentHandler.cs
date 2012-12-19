using AnecdoteByExampleTwo.Application;

namespace AnecdoteByExampleTwo.ServiceAdapter
{
    public class ServicePaymentHandler : IPaymentHandler
    {
        public PaymentReceipt Handle(Payment payment)
        {
            return new SuccessReceipt();
        }
    }
}
