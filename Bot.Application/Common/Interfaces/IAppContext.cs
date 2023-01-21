using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Common.Interfaces;

public interface IAppContext
{
    DbSet<Quiz> Quizes { get; }
    DbSet<Event> Events { get; }
    DbSet<EventUser> EventUsers { get; }
    DbSet<Reward> Rewards { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
