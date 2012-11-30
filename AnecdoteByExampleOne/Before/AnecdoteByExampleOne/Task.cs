namespace AnecdoteByExampleOne
{
    public class Task
    {
        readonly IHandle<Something, Query> _queryHandler;
        readonly IHandle<FirstCommand> _firstCommandHandler;
        readonly IHandle<SecondCommand> _secondCommandHandler;

        public Task(
            IHandle<Something, Query> queryHandler,
            IHandle<FirstCommand> firstCommandHandler,
            IHandle<SecondCommand> secondCommandHandler)
        {
            _queryHandler = queryHandler;
            _firstCommandHandler = firstCommandHandler;
            _secondCommandHandler = secondCommandHandler;
        }

        public void Execute()
        {
            var payload = _queryHandler.Handle(new Query());

            if (payload != null)
            {
                if (_firstCommandHandler.Handle(new FirstCommand(payload)) is CommandSucceeded)
                {
                    _secondCommandHandler.Handle(new SecondCommand());
                }
            }
        }
    }
}