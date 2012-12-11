namespace AnecdoteByExampleTwo.Domain
{
    public interface IHandle { }

    public interface IHandle<T> : IHandle where T : Event
    {
        void Handle(T @event);
    }
}