using Bot.Application.Event.Commands;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.Commons;
using System.Net;

namespace Bot.IntegrationTests;

public class EventControllerTest : ClientFixture
{
    public EventControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}


    [Theory]
    [InlineData("api/v1/Event?PageNumber=1&PageSize=1")]
    public async Task GetEventPaginated_Should_Return200Ok(string url)
    {
        await SeedWork.AddEvents();

        var response = await AsGetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("api/v1/Event/find-active?query=1001")]
    public async Task GetActiveEventById_Should_Return200Ok(string url)
    {
        await SeedWork.AddEvents();
        var response = await AsGetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateEvent_Should_Return201Created()
    {
        CreateEventCommand @event = new()
        {
            DateStart = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(3),
            Description = "Event about general knowlodge",
        };

        var response = await AsPostAsync("api/v1/Event", @event);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task UpdateEvent_Should_Return200Ok()
    {
        await SeedWork.AddEvents();
        
        UpdateEventCommand @event = new()
        {
            Id = 1001,
            DateStart = DateTime.UtcNow,
            Description = "Event about general knowlodge",
            IsActive = false
        };

        var response = await AsPutAsync("api/v1/Event", @event);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
