using Entities = Bot.Domain.Entities;

namespace Domain.Events.Reward;

public class RewardCreatedEvent : BaseEvent
{
    public RewardCreatedEvent(Entities.Reward item)
    {
        Item = item;
    }

    public Entities.Reward Item { get; set; }
}
