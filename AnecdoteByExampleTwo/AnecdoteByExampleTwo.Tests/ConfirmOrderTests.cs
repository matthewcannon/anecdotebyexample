using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnecdoteByExampleTwo.Tests
{
    [TestFixture]
    public class always : IHandle<IEvent>
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

        public void Handle(IEvent @event)
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
        readonly IList<IHandle<IEvent>> _eventHandlers;

        public EventAggregator()
        {
            _eventHandlers = new List<IHandle<IEvent>>();    
        }

        public void Publish(IEvent @event)
        {
            foreach(var eventHandler in _eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }

        public void Register<T>(T eventHandler) where T : IHandle<IEvent>
        {
            _eventHandlers.Add(eventHandler);
        }
    }

    internal interface IHandle<T> where T : IEvent
    {
        void Handle(T @event);
    }

    public class OrderConfirmationEmail : Email {}

    public class EmailSent<T> : IEvent where T : Email {}

    public class Email {}

    public interface IEvent {}
}
