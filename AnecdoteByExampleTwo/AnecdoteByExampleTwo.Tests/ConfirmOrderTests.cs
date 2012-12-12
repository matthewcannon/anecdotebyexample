using System.Collections.Generic;
using System.Linq;
using AnecdoteByExampleTwo.Application;
using AnecdoteByExampleTwo.Factory;
using NUnit.Framework;

namespace AnecdoteByExampleTwo.Tests
{
    [TestFixture]
    public class payment_is_accepted : IHandle<EmailSent<OrderConfirmationEmail>>, IPaymentHandler
    {
        IList<Event> _events;
        EventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            _events = new List<Event>();
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
        }

        [Test]
        public void order_confirmation_email_is_sent()
        {
            new TaskFactory().ConfirmOrder(_eventAggregator).Execute(new Order("customer@customer.com"));

            Assert.That(_events.OfType<EmailSent<OrderConfirmationEmail>>().Count(), Is.EqualTo(1));
        }

        public void Handle(EmailSent<OrderConfirmationEmail> @event)
        {
            _events.Add(@event);
        }

        public PaymentStatus Handle(Payment payment)
        {
            return new PaymentAccepted();
        }
    }

    [TestFixture]
    public class payment_is_rejected : IHandle<EmailSent<OrderConfirmationEmail>>
    {
        IList<Event> _events;
        EventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            _events = new List<Event>();
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
        }

        [Test]
        public void order_confirmation_email_is_not_sent()
        {
            new TaskFactory().ConfirmOrder(_eventAggregator).Execute(new Order("customer@customer.com"));
            Assert.That(_events.OfType<EmailSent<OrderConfirmationEmail>>().Count(), Is.EqualTo(0));
        }

        public void Handle(EmailSent<OrderConfirmationEmail> @event)
        {
            _events.Add(@event);
        }
    }
}