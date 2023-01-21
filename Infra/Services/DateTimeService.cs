using Bot.Application.Common.Interfaces;

namespace Bot.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
