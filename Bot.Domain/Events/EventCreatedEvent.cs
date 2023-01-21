namespace Domain.Events;

public class EventCreatedEvent : BaseEvent
{
    public Event Item { get; }
    public EventCreatedEvent(Event @event)
    {
        Item= @event;
    }
}
