using System.Collections.Generic;

namespace AnecdoteByExampleOne
{
    public interface IHandle<T1> where T1 : Command
    {
        CommandCompleted Handle(T1 command);
    }

    public interface IHandle<T1, T2> where T2 : Query
    {
        IEnumerable<T1> Handle(T2 query);
    }
}