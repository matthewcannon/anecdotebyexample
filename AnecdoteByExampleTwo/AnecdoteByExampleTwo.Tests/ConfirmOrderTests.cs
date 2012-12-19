using System.Collections.Generic;
using System.Linq;
using AnecdoteByExampleTwo.Application;
using AnecdoteByExampleTwo.Application.Events;
using AnecdoteByExampleTwo.Factory;
using NUnit.Framework;

namespace AnecdoteByExampleTwo.Tests
{
    [TestFixture]
    public class payment_is_accepted : IHandle<EmailSent>, IEmailSender, IPaymentHandler
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
        public void email_is_sent()
        {
            var order = new Order("customer@customer.com", new Payment());
            new TaskFactory().ConfirmOrder(_eventAggregator, this, this).Execute(order);
            Assert.That(_events.OfType<EmailSent>().Count(), Is.EqualTo(1));
        }

        public void Handle(EmailSent @event)
        {
            _events.Add(@event);
        }

        public void SendEmail(Email email) { }

        public void Handle(Payment payment) { }
    }
    
    [TestFixture]
    public class payment_is_rejected : IHandle<EmailSent>, IEmailSender, IPaymentHandler
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
        public void email_is_not_sent()
        {
            var order = new Order("customer@customer.com", new Payment());
            new TaskFactory().ConfirmOrder(_eventAggregator, this, this).Execute(order);
            Assert.That(_events.OfType<EmailSent>().Count(), Is.EqualTo(0));
        }

        public void Handle(EmailSent @event)
        {
            _events.Add(@event);
        }

        public void SendEmail(Email email) { }

        public void Handle(Payment payment) { }
    }
}