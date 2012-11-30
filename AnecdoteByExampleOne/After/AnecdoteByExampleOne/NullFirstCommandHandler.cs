namespace AnecdoteByExampleOne
{
    public class NullFirstCommandHandler : Commands.IHandle<FirstCommand>
    {
        readonly EventAggregator _eventAggregator;

        public NullFirstCommandHandler(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(FirstCommand command)
        {
            _eventAggregator.Publish(new CommandNotExecuted<FirstCommand>());
        }
    }
}