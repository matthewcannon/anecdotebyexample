using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnecdoteByExampleOne.Tests
{
    [TestFixture]
    public class query_finds_nothing : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<CommandNotExecuted<FirstCommand>>, Events.IHandle<CommandNotExecuted<SecondCommand>>
    {
        EventAggregator _eventAggregator;
        IList<CommandNotExecuted<FirstCommand>> _firstCommandNotExecutedEvents;
        IList<CommandNotExecuted<SecondCommand>> _secondCommandNotExecutedEvents;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register<CommandNotExecuted<FirstCommand>>(this);
            _firstCommandNotExecutedEvents = new List<CommandNotExecuted<FirstCommand>>();
            _secondCommandNotExecutedEvents = new List<CommandNotExecuted<SecondCommand>>();
        }

        [Test]
        public void first_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_firstCommandNotExecutedEvents.Count(), Is.EqualTo(1));
        }

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_secondCommandNotExecutedEvents.Count(), Is.EqualTo(1));
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

        public void Handle(CommandNotExecuted<FirstCommand> firstCommandNotExecuted)
        {
            _firstCommandNotExecutedEvents.Add(firstCommandNotExecuted);
        }

        public void Handle(CommandNotExecuted<SecondCommand> secondCommandNotExecuted)
        {
            _secondCommandNotExecutedEvents.Add(secondCommandNotExecuted);
        }
    }

    [TestFixture]
    public class query_finds_something : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<CommandExecuted<FirstCommand>>
    {
        EventAggregator _eventAggregator;
        IList<CommandExecuted<FirstCommand>> _firstCommandExecutedEvents;
        Something _somethingFound;
        FirstCommand _commandExecuted;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _firstCommandExecutedEvents = new List<CommandExecuted<FirstCommand>>();
        }

        [Test]
        public void first_command_is_executed_once()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_firstCommandExecutedEvents.Count(), Is.EqualTo(1));
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

        public void Handle(CommandExecuted<FirstCommand> firstCommandExecuted)
        {
            _firstCommandExecutedEvents.Add(firstCommandExecuted);
        }
    }

    [TestFixture]
    public class first_command_succeeds : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<CommandExecuted<SecondCommand>>
    {
        EventAggregator _eventAggregator;
        IList<CommandExecuted<SecondCommand>> _secondCommandExecutedEvents;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _secondCommandExecutedEvents = new List<CommandExecuted<SecondCommand>>();
        }

        [Test]
        public void second_command_is_executed_once()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_secondCommandExecutedEvents.Count(), Is.EqualTo(1));
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

        public void Handle(CommandExecuted<SecondCommand> secondCommandExecuted)
        {
            _secondCommandExecutedEvents.Add(secondCommandExecuted);
        }
    }

    [TestFixture]
    public class first_command_fails : Queries.IHandle<Query>, Commands.IHandle<FirstCommand>, Commands.IHandle<SecondCommand>, Events.IHandle<CommandNotExecuted<SecondCommand>>
    {
        EventAggregator _eventAggregator;
        IList<CommandNotExecuted<SecondCommand>> _secondCommandNotExcecutedEvents;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Register(this);
            _secondCommandNotExcecutedEvents = new List<CommandNotExecuted<SecondCommand>>();
        }

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(_eventAggregator, this, this, this).Execute();

            Assert.That(_secondCommandNotExcecutedEvents.Count(), Is.EqualTo(1));
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

        public void Handle(CommandNotExecuted<SecondCommand> secondCommandNotExecuted)
        {
            _secondCommandNotExcecutedEvents.Add(secondCommandNotExecuted);
        }
    }
}