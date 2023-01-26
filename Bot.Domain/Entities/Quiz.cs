using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class Quiz : BaseAuditiableEntity
{
    public string? Title { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string? Hint { get; set; }
    public int FkEvent { get; set; }
    public bool HasNextQuestion { get; set; }
}

