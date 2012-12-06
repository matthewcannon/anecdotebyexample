using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnecdoteByExampleTwo.Tests
{
    [TestFixture]
    public class one_order_confirmation_email_is_sent
    {
        IList<IEvent> _events;

        [SetUp]
        public void SetUp()
        {
            _events = new List<IEvent>();    
        }

        [Test]
        public void test()
        {
            new ConfirmOrder().Execute();
            OneOrderConfirmationEmailIsSent();
        }

        void OneOrderConfirmationEmailIsSent()
        {
            Assert.That(_events.Where(e => e is EmailSent<OrderConfirmationEmail>).Count(), Is.EqualTo(1));
        }
    }

    public class ConfirmOrder
    {
        public void Execute()
        {
        }
    }

    internal class OrderConfirmationEmail : Email {}

    internal class EmailSent<T> : IEvent where T : Email {}

    internal class Email {}

    internal interface IEvent {}
}
