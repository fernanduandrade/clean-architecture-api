namespace Bot.Application.EventUser.DTO;

public record EventUserDTO
{
    public int Id { get; init; }
    public int EventId { get; init; }
    public string? UserId { get; init; }
}
