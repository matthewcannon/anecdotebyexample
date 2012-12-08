using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnecdoteByExampleTwo.Tests
{
    [TestFixture]
    public class always : IHandle<EmailSent<OrderConfirmationEmail>>
    {
        IList<IEvent> _events;
        EventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            _events = new List<IEvent>();
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
        }

        [Test]
        public void an_order_confirmation_email_is_sent()
        {
            new ConfirmOrder(_eventAggregator).Execute();
            OneOrderConfirmationEmailIsSent();
        }

        void OneOrderConfirmationEmailIsSent()
        {
            Assert.That(_events.Where(e => e is EmailSent<OrderConfirmationEmail>).Count(), Is.EqualTo(1));
        }

        public void Handle(EmailSent<OrderConfirmationEmail> @event)
        {
            _events.Add(@event);
        }
    }

    internal class ConfirmOrder
    {
        readonly EventAggregator _eventAggregator;

        public ConfirmOrder(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            _eventAggregator.Publish(new EmailSent<OrderConfirmationEmail>());
        }
    }

    internal class EventAggregator
    {
        readonly IList<IHandle> _eventHandlers;

        public EventAggregator()
        {
            _eventHandlers = new List<IHandle>();    
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            foreach (var eventHandler in _eventHandlers.OfType<IHandle<T>>())
            {
                eventHandler.Handle(@event);
            }
        }

        public void Register<T>(IHandle<T> eventHandler) where T : IEvent
        {
            _eventHandlers.Add(eventHandler);
        }
    }

    internal interface IHandle { }

    internal interface IHandle<T> : IHandle where T : IEvent
    {
        void Handle(T @event);
    }

    public class OrderConfirmationEmail : Email {}

    public class EmailSent<T> : IEvent where T : Email {}

    public class Email {}

    public interface IEvent {}

    public class Nothing : IEvent { }
}