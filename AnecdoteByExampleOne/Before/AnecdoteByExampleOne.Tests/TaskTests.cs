using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace AnecdoteByExampleOne.Tests
{
    [TestFixture]
    public class query_returns_nothing : IHandle<string, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private bool _firstCommandExecuted;

        [Test]
        public void first_command_is_not_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_firstCommandExecuted, Is.False);
        }

        public IEnumerable<string> Handle(Query query)
        {
            return null;
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            _firstCommandExecuted = true;

            return new CommandCompleted();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class query_returns_something : IHandle<string, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private bool _firstCommandExecuted;

        [Test]
        public void first_command_is_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_firstCommandExecuted, Is.True);
        }

        public IEnumerable<string> Handle(Query query)
        {
            return new Collection<string>();
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            _firstCommandExecuted = true;

            return new CommandCompleted();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class first_command_succeeds : IHandle<string, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private bool _secondCommandExecuted;

        [Test]
        public void second_command_is_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_secondCommandExecuted, Is.True);
        }

        public IEnumerable<string> Handle(Query query)
        {
            return new Collection<string>();
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            return new CommandSucceeded();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            _secondCommandExecuted = true;

            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class first_command_fails : IHandle<string, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private bool _secondCommandExecuted;

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_secondCommandExecuted, Is.False);
        }

        public IEnumerable<string> Handle(Query query)
        {
            return new Collection<string>();
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            return new CommandFailed();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            _secondCommandExecuted = true;

            return new CommandCompleted();
        }
    }
}