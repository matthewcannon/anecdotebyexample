using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnecdoteByExampleOne.Tests
{
    [TestFixture]
    public class query_finds_nothing : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<Event>
    {
        EventAggregator _eventAggregator;
        IList<Event> _events;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _events = new List<Event>();
        }

        [Test]
        public void first_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_events.Where(e => e is CommandNotExecuted<FirstCommand>).Count(), Is.EqualTo(1));
        }

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_events.Where(e => e is CommandNotExecuted<SecondCommand>).Count(), Is.EqualTo(1));
        }

        public void Handle(Query query)
        {
            _eventAggregator.Publish(new Found<Nothing>());
        }

        public void Handle(FirstCommand command)
        {
            _eventAggregator.Publish(new CommandExecuted<FirstCommand>());
        }

        public void Handle(SecondCommand command)
        {
            _eventAggregator.Publish(new CommandExecuted<SecondCommand>());            
        }

        public void Handle(Event @event)
        {
            _events.Add(@event);
        }
    }

    [TestFixture]
    public class query_finds_something : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<Event>
    {
        EventAggregator _eventAggregator;
        IList<Event> _events;
        private Something _somethingFound;
        private FirstCommand _commandExecuted;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _events = new List<Event>();
        }

        [Test]
        public void first_command_is_executed_once()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_events.Where(e => e is CommandExecuted<FirstCommand>).Count(), Is.EqualTo(1));
        }

        [Test]
        public void payload_of_command_executed_is_something_found()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_commandExecuted.Payload, Is.EqualTo(_somethingFound));
        }

        public void Handle(Query query)
        {
            _somethingFound = new Something();

            _eventAggregator.Publish(new Found<Something>(_somethingFound));
        }

        public void Handle(FirstCommand command)
        {
            _commandExecuted = command;

            _eventAggregator.Publish(new CommandExecuted<FirstCommand>());
        }

        public void Handle(SecondCommand command)
        {
            _eventAggregator.Publish(new CommandExecuted<SecondCommand>());
        }

        public void Handle(Event @event)
        {
            _events.Add(@event);
        }
    }

    [TestFixture]
    public class first_command_succeeds : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<Event>
    {
        EventAggregator _eventAggregator;
        IList<Event> _events;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _events = new List<Event>();
        }

        [Test]
        public void second_command_is_executed_once()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_events.Where(e => e is CommandExecuted<SecondCommand>).Count(), Is.EqualTo(1));
        }

        public void Handle(Query query)
        {
            _eventAggregator.Publish(new Found<Something>(new Something()));
        }

        public void Handle(FirstCommand command)
        {
            _eventAggregator.Publish(new CommandSucceeded<FirstCommand>());
        }

        public void Handle(SecondCommand command)
        {
            _eventAggregator.Publish(new CommandExecuted<SecondCommand>());
        }

        public void Handle(Event @event)
        {
            _events.Add(@event);
        }
    }

    [TestFixture]
    public class first_command_fails : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<Event>
    {
        EventAggregator _eventAggregator;
        IList<Event> _events;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _events = new List<Event>();
        }

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_events.Where(e => e is CommandNotExecuted<SecondCommand>).Count(), Is.EqualTo(1));
        }

        public void Handle(Query query)
        {
            _eventAggregator.Publish(new Found<Something>(new Something()));
        }

        public void Handle(FirstCommand command)
        {
            _eventAggregator.Publish(new CommandFailed<FirstCommand>());
        }

        public void Handle(SecondCommand command)
        {
            _eventAggregator.Publish(new CommandExecuted<SecondCommand>());
        }

        public void Handle(Event @event)
        {
            _events.Add(@event);
        }
    }
}