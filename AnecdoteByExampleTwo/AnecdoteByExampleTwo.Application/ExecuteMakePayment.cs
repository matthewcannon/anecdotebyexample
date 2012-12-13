namespace AnecdoteByExampleTwo.Application
{
    public class ExecuteMakePayment
    {
        readonly EventAggregator _eventAggregator;
        readonly IPaymentHandler _paymentHandler;

        public ExecuteMakePayment(EventAggregator eventAggregator, IPaymentHandler paymentHandler)
        {
            _eventAggregator = eventAggregator;
            _paymentHandler = paymentHandler;
        }

        public void Execute(MakePayment makePayment)
        {
            _paymentHandler.Handle(makePayment.Payment);
        }
    }
}