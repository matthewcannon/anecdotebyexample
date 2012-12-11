namespace AnecdoteByExampleOne
{
    public interface IHandle { }

    namespace Commands
    {
        public interface IHandle<T1> : IHandle where T1 : Command
        {
            void Handle(T1 command);
        }
    }   

    namespace Queries
    {
        public interface IHandle<T1> : IHandle where T1 : Query
        {
            void Handle(T1 query);
        }
    }

    namespace Events
    {
        public interface IHandle<T1> : IHandle where T1 : Event
        {
            void Handle(T1 secondCommandNotExecuted);
        }
    }
}