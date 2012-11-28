namespace AnecdoteByExampleOne
{
    public class Task
    {
        readonly IHandle<string, Query> _queryHandler;
        readonly IHandle<FirstCommand> _firstCommandHandler;
        readonly IHandle<SecondCommand> _secondCommandHandler;

        public Task(
            IHandle<string, Query> queryHandler,
            IHandle<FirstCommand> firstCommandHandler,
            IHandle<SecondCommand> secondCommandHandler)
        {
            _queryHandler = queryHandler;
            _firstCommandHandler = firstCommandHandler;
            _secondCommandHandler = secondCommandHandler;
        }

        public void Execute()
        {
            var queryResult = _queryHandler.Handle(new Query());

            if (queryResult != null)
            {
                if (_firstCommandHandler.Handle(new FirstCommand()) is CommandSucceeded)
                {
                    _secondCommandHandler.Handle(new SecondCommand());
                }
            }
        }
    }
}