namespace AnecdoteByExampleOne
{
    public class NullSecondCommandHandler : Commands.IHandle<SecondCommand>
    {
        readonly EventAggregator _eventAggregator;

        public NullSecondCommandHandler(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(SecondCommand command)
        {
            _eventAggregator.Publish(new CommandNotExecuted<SecondCommand>());
        }
    }
}