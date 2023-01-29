namespace Bot.Domain.Events;

public class RewardClaimEvent : BaseEvent
{
    public Reward Item { get; }
    public RewardClaimEvent(Reward reward)
    {
        Item = reward;
    }
}
