namespace Domain.Events;

public class EventUserCreatedEvent : BaseEvent
{
    public EventUser Item { get; }

    public EventUserCreatedEvent(EventUser item)
    {
        Item = item;
    }

}
