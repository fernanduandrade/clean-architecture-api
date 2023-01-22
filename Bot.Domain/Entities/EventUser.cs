using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class EventUser : BaseAuditiableEntity
{
    public int FkEvent { get; set; }
    public string? FkUser { get; set; }
}
