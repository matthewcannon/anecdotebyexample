using NUnit.Framework;

namespace AnecdoteByExampleOne.Tests
{
    [TestFixture]
    public class query_finds_nothing : IHandle<Something, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private FirstCommand _firstCommand;
        private SecondCommand _secondCommand;

        [Test]
        public void first_command_is_not_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_firstCommand, Is.Null);
        }
        
        [Test]
        public void second_command_is_not_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_secondCommand, Is.Null);
        }

        public Something Handle(Query query)
        {
            return null;
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            _firstCommand = command;

            return new CommandCompleted();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            _secondCommand = command;

            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class query_finds_something : IHandle<Something, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private FirstCommand _firstCommand;
        private Something _somethingFound;

        [Test]
        public void first_command_is_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_firstCommand, Is.Not.Null);
        }

        [Test]
        public void payload_of_command_executed_is_something_found()
        {
            new Task(this, this, this).Execute();

            Assert.That(_firstCommand.Payload, Is.EqualTo(_somethingFound));
        }

        public Something Handle(Query query)
        {
            _somethingFound = new Something();

            return _somethingFound;
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            _firstCommand = command;

            return new CommandCompleted();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class first_command_succeeds : IHandle<Something, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private SecondCommand _secondCommand;

        [Test]
        public void second_command_is_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_secondCommand, Is.Not.Null);
        }

        public Something Handle(Query query)
        {
            return new Something();
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            return new CommandSucceeded();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            _secondCommand = command;

            return new CommandCompleted();
        }
    }

    [TestFixture]
    public class first_command_fails : IHandle<Something, Query>, IHandle<FirstCommand>, IHandle<SecondCommand>
    {
        private SecondCommand _secondCommand;

        [Test]
        public void second_command_is_not_executed()
        {
            new Task(this, this, this).Execute();

            Assert.That(_secondCommand, Is.Null);
        }

        public Something Handle(Query query)
        {
            return new Something();
        }

        public CommandCompleted Handle(FirstCommand command)
        {
            return new CommandFailed();
        }

        public CommandCompleted Handle(SecondCommand command)
        {
            _secondCommand = command;

            return new CommandCompleted();
        }
    }
}