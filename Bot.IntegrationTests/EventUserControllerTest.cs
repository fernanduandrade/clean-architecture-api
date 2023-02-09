using System.Net;
using Bot.Application.EventUser.Commands;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.Commons;

namespace Bot.IntegrationTests;

public class EventUserControllerTest : ClientFixture
{
    public EventUserControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}

    [Theory]
    [InlineData("2001")]
    public async Task DeleteEventUser_Should_Return_200Ok(string id)
    {
        await SeedWork.AddEvents();
        await SeedWork.AddEventUsers();

        var url = $"api/v1/EventUser?Id={id}";

        var response = await AsDeleteAsync(url);

        Assert.Equal(HttpStatusCode.OK,  response.StatusCode);
    }

    [Theory]
    [InlineData("985216762557644891")]
    public async Task GetCheckUser_Should_Return_200Ok(string id)
    {
        await SeedWork.AddEvents();
        await SeedWork.AddEventUsers();

        var url = $"api/v1/EventUser/check-user?UserDiscordId={id}";

        var response = await AsGetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateEventUser_Should_Return_200Ok()
    {
        CreateEventUserCommand request = new()
        {
            EventId = 1002,
            UserDiscordId = "1781398562",
        };

        var response = await AsPostAsync("api/v1/EventUser", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
