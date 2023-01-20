using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class Event : BaseAuditiableEntity
{
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int FkReWard { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime ExpireAt { get; set; }

}