using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class Reward : BaseAuditiableEntity
{
    public int Coin { get; set; }
    public int Expirience { get; set; }
    public bool Claimed { get; set; }
    public string? Role { get; set; }
    public int FkEvent { get; set; }
    public bool ParticipantReward { get; set; }
}
