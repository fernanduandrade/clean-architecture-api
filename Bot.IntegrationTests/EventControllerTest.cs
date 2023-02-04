using Bot.Application.Event.Commands;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.Commons;
using System.Net;
using Bot.IntegrationTests.Setup;

namespace Bot.IntegrationTests;

public class EventControllerTest : ClientFixture
{
    public EventControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}


    [Theory]
    [InlineData("api/v1/Event?PageNumber=1&PageSize=1")]
    public async Task GetEventPaginated_Should_Return200Ok(string url)
    {
        var response = await AsGetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("api/v1/Event/find-active?query=1")]
    public async Task GetActiveEventById_Should_Return200Ok(string url)
    {

        var response = await AsGetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateEvent_Should_Return200Ok()
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
        UpdateEventCommand @event = new()
        {
            Id = 1,
            DateStart = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(3),
            Description = "Event about general knowlodge",
            IsActive = true
        };

        var response = await AsPutAsync("api/v1/Event", @event);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
