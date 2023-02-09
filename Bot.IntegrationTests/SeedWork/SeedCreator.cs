using Entities = Bot.Domain.Entities;
using Bot.Infrastructure.Persistence;

namespace Bot.IntegrationTests.SeedWork;

public class SeedCreator
{
    private readonly AppDbContext _context;

    public SeedCreator(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedDb()
    {
        var events = new List<Entities.Event>
        {
            new Entities.Event{ Id = 1001, IsActive = true , DateStart = DateTime.UtcNow.AddDays(23), ExpireAt = DateTime.UtcNow.AddDays(37), Description = "Evento sobre história da he4rt" },
            new Entities.Event{ Id = 1002, IsActive = false , DateStart = DateTime.UtcNow.AddDays(21), ExpireAt = DateTime.UtcNow.AddDays(22), Description = "Coding quiz" }
        };

        var rewards = new List<Entities.Reward>
        {
            new Entities.Reward{ Id = 2001, Claimed = false , ParticipantReward = false, Coin = 9302, FkEvent = 1001, Expirience = 3222, Role = "test" },
            new Entities.Reward{ Id = 2002, Claimed = false , ParticipantReward = false, Coin = 8872, FkEvent = 1001, Expirience = 2231, Role = "test" },
            new Entities.Reward{ Id = 2003, Claimed = false , ParticipantReward = false, Coin = 5002, FkEvent = 1001, Expirience = 2877, Role = "test" },
            new Entities.Reward{ Id = 2004, Claimed = false , ParticipantReward = true, Coin = 1301, FkEvent = 1001, Expirience = 1022, Role = "test" },
            new Entities.Reward{ Id = 2005, Claimed = false , ParticipantReward = false, Coin = 10039, FkEvent = 1002, Expirience = 3227, Role = "test" },
            new Entities.Reward{ Id = 2006, Claimed = false , ParticipantReward = false, Coin = 9122, FkEvent = 1002, Expirience = 2322, Role = "test" },
            new Entities.Reward{ Id = 2007, Claimed = false , ParticipantReward = false, Coin = 5323, FkEvent = 1002, Expirience = 1800, Role = "test" },
            new Entities.Reward{ Id = 2008, Claimed = false , ParticipantReward = true, Coin = 1401, FkEvent = 1002, Expirience = 1022, Role = "test" },
        };

        var eventsUsers = new List<Entities.EventUser>
        {
            new Entities.EventUser{ Id = 101, FkEvent = 1001, FkUser = "985216762557644891" },
            new Entities.EventUser{ Id = 102, FkEvent = 1001, FkUser = "985216762557644892" },
            new Entities.EventUser{ Id = 103, FkEvent = 1001, FkUser = "985216762557644893" },
            new Entities.EventUser{ Id = 104, FkEvent = 1002, FkUser = "985216762557644896" },
            new Entities.EventUser{ Id = 105, FkEvent = 1002, FkUser = "985216762557644898" }
        };

        await _context.Events.AddRangeAsync(events);
        await _context.EventUsers.AddRangeAsync(eventsUsers);
        await _context.Rewards.AddRangeAsync(rewards);
        await _context.SaveChangesAsync();

    }

    public async Task AddEvents()
    {
        var events = new List<Entities.Event>
        {
            new Entities.Event{ Id = 1001, IsActive = true , DateStart = DateTime.UtcNow.AddDays(23), ExpireAt = DateTime.UtcNow.AddDays(37), Description = "Evento sobre história da he4rt", Created = DateTime.UtcNow, CreatedBy = "Fernando" },
            new Entities.Event{ Id = 1002, IsActive = false , DateStart = DateTime.UtcNow.AddDays(21), ExpireAt = DateTime.UtcNow.AddDays(22), Description = "Coding quiz", Created = DateTime.UtcNow, CreatedBy = "Fernando" }
        };

        await _context.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    public async Task AddRewards()
    {
        var rewards = new List<Entities.Reward>
        {
            new Entities.Reward{ Id = 2001, Claimed = false , ParticipantReward = false, Coin = 9302, FkEvent = 1001, Expirience = 3222, Role = "test" },
            new Entities.Reward{ Id = 2002, Claimed = false , ParticipantReward = false, Coin = 8872, FkEvent = 1001, Expirience = 2231, Role = "test" },
            new Entities.Reward{ Id = 2003, Claimed = false , ParticipantReward = false, Coin = 5002, FkEvent = 1001, Expirience = 2877, Role = "test" },
            new Entities.Reward{ Id = 2004, Claimed = false , ParticipantReward = true, Coin = 1001, FkEvent = 1001, Expirience = 1022, Role = "test" },
            new Entities.Reward{ Id = 2005, Claimed = false , ParticipantReward = false, Coin = 10039, FkEvent = 1002, Expirience = 3227, Role = "test" },
            new Entities.Reward{ Id = 2006, Claimed = false , ParticipantReward = false, Coin = 9122, FkEvent = 1002, Expirience = 2322, Role = "test" },
            new Entities.Reward{ Id = 2007, Claimed = false , ParticipantReward = false, Coin = 5323, FkEvent = 1002, Expirience = 1800, Role = "test" },
            new Entities.Reward{ Id = 2008, Claimed = false , ParticipantReward = true, Coin = 1001, FkEvent = 1002, Expirience = 1022, Role = "test" },
        };

        await _context.Rewards.AddRangeAsync(rewards);
        await _context.SaveChangesAsync();
    }

    public async Task AddEventUsers()
    {
        var eventsUsers = new List<Entities.EventUser>
        {
            new Entities.EventUser{ Id = 2001, FkEvent = 1001, FkUser = "985216762557644891" },
            new Entities.EventUser{ Id = 2002, FkEvent = 1001, FkUser = "985216762557644892" },
            new Entities.EventUser{ Id = 2003, FkEvent = 1001, FkUser = "985216762557644893" },
            new Entities.EventUser{ Id = 2004, FkEvent = 1002, FkUser = "985216762557644896" },
            new Entities.EventUser{ Id = 2005, FkEvent = 1002, FkUser = "985216762557644898" }
        };

            await _context.EventUsers.AddRangeAsync(eventsUsers);
            await _context.SaveChangesAsync(cancellationToken: default);
    }

}
