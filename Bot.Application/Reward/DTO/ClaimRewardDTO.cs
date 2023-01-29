namespace Bot.Application.Reward.DTO;

public record ClaimRewardDTO
{
    public int Id { get; init; }
    public int Coins { get; init; }
    public int Expirience { get; init; }
    public string? Role { get; init; }
}
