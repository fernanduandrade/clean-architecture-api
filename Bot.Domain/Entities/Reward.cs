using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class Reward : BaseAuditiableEntity
{
    public int Coin { get; set; }
    public int Xp { get; set; }
    public bool Earned { get; set; }
    public string? Role { get; set; }
    public bool ParticipantReward { get; set; }
}
