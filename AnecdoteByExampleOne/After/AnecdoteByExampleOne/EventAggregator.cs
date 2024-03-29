using System.Collections.Generic;
using System.Linq;

namespace AnecdoteByExampleOne
{
    public class EventAggregator
    {
        readonly IList<IHandle> _eventHandlers;

        public EventAggregator()
        {
            _eventHandlers = new List<IHandle>();
        }

        public void Publish<T>(T @event) where T : Event
        {
            foreach (var eventHandler in _eventHandlers.OfType<Events.IHandle<T>>())
            {
                eventHandler.Handle(@event);
            }
        }

        public void Register<T>(Events.IHandle<T> eventHandler) where T : Event
        {
            _eventHandlers.Add(eventHandler);
        }
    }
}