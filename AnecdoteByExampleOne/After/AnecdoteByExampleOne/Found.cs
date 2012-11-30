namespace AnecdoteByExampleOne
{
    public class Found<T> : Event
    {
        public T ThingFound { get; private set; }

        public Found() { }

        public Found(T thingFound)
        {
            ThingFound = thingFound;
        }
    }
}