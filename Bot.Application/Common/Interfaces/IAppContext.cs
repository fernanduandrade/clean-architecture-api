using Microsoft.EntityFrameworkCore;
using Entities = Bot.Domain.Entities;
namespace Bot.Application.Common.Interfaces;

public interface IAppContext
{
    DbSet<Entities.Quiz> Quizes { get; }
    DbSet<Entities.Event> Events { get; }
    DbSet<Entities.EventUser> EventUsers { get; }
    DbSet<Entities.Reward> Rewards { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
