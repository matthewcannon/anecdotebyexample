using System.Collections.Generic;

namespace AnecdoteByExampleOne
{
    public class EventAggregator
    {
        readonly IList<Events.IHandle<Event>> _eventHandlers = new List<Events.IHandle<Event>>();
 
        public void Publish(Event @event)
        {
            foreach (var eventHandler in _eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }

        public void Register(Events.IHandle<Event> eventHandler)
        {
            _eventHandlers.Add(eventHandler);
        }
    }
}