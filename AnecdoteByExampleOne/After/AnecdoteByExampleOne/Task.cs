namespace AnecdoteByExampleOne
{
    public class Task : Events.IHandle<Found<Nothing>>, Events.IHandle<Found<Something>>, Events.IHandle<CommandFailed<FirstCommand>>
    {
        readonly EventAggregator _eventAggregator;
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
            _eventAggregator.Register<Found<Nothing>>(this);
            _eventAggregator.Register<Found<Something>>(this);
            _eventAggregator.Register<CommandFailed<FirstCommand>>(this);

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

        public void Handle(Found<Nothing> foundNothing)
        {
            _firstCommandHandler = new NullFirstCommandHandler(_eventAggregator);
            _secondCommandHandler = new NullSecondCommandHandler(_eventAggregator);                 }

        public void Handle(Found<Something> foundSomething)
        {
            _payload = foundSomething.ThingFound;
        }

        public void Handle(CommandFailed<FirstCommand> firstCommandFailed)
        {
            _secondCommandHandler = new NullSecondCommandHandler(_eventAggregator);
        }
    }
}