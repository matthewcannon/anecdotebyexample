using AnecdoteByExampleTwo.Application.Commands;

namespace AnecdoteByExampleTwo.Application.CommandHandlers
{
    public class HandleMakePayment
    {
        readonly EventAggregator _eventAggregator;
        readonly IPaymentHandler _paymentHandler;

        public HandleMakePayment(EventAggregator eventAggregator, IPaymentHandler paymentHandler)
        {
            _eventAggregator = eventAggregator;
            _paymentHandler = paymentHandler;
        }

        public void Handle(MakePayment makePayment)
        {
            var paymentReceipt = _paymentHandler.Handle(makePayment.Payment);

            if (paymentReceipt is FailureReceipt) _eventAggregator.Publish(new Events.PaymentRejected());
        }
    }
}