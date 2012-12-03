namespace AnecdoteByExampleOne
{
    public class Task : Events.IHandle<Event>
    {
        private readonly EventAggregator _eventAggregator;
        readonly Queries.IHandle<Query> _queryHandler;
        Commands.IHandle<FirstCommand> _firstCommandHandler;
        Commands.IHandle<SecondCommand> _secondCommandHandler;
        Something _payload;

        public Task(
            EventAggregator eventAggregator,
            Queries.IHandle<Query> queryHandler,
            Commands.IHandle<FirstCommand> firstCommandHandler,
            Commands.IHandle<SecondCommand> secondCommandHandler)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Register(this);

            _queryHandler = queryHandler;
            _firstCommandHandler = firstCommandHandler;
            _secondCommandHandler = secondCommandHandler;
        }

        public void Execute()
        {
            _queryHandler.Handle(new Query());
        
            _firstCommandHandler.Handle(new FirstCommand(_payload));
            
            _secondCommandHandler.Handle(new SecondCommand());
        }

        public void Handle(Event @event)
        {
            if (@event is Found<Nothing>)
            {
                _firstCommandHandler = new NullFirstCommandHandler(_eventAggregator);
                _secondCommandHandler = new NullSecondCommandHandler(_eventAggregator);
            }

            if (@event is Found<Something>) _payload = (@event as Found<Something>).ThingFound;

            if (@event is CommandFailed<FirstCommand>) _secondCommandHandler = new NullSecondCommandHandler(_eventAggregator);
        }
    }
}