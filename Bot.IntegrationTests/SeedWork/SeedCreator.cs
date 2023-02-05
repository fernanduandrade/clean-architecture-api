using Bot.Infrastructure.Persistence;

namespace Bot.IntegrationTests.Setup;

public class SeedCreator
{
    private readonly AppDbContext _context;

    public SeedCreator(AppDbContext context)
    {
        _context = context;
    }

    // TODO criar seeds
    public async Task AddEvents()
    {
    }
}
