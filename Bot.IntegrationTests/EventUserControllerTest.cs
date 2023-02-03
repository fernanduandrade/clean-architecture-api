using Bot.Application.Event.Commands;
using Bot.Application.EventUser.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Bot.IntegrationTests;

public class EventUserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EventUserControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    public async Task DeleteEventUser_Should_Return_200Ok(string id)
    {
        var url = $"api/v1/EventUser?Id={id}";

        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Theory]
    [InlineData("2129463319")]
    [InlineData("1365659938")]
    [InlineData("1781398562")]
    public async Task GetCheckUser_Should_Return_200Ok(string id)
    {
        var url = $"api/v1/EventUser/check-user?UserDiscordId={id}";

        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateEventUser_Should_Return_200Ok()
    {
        CreateEventUserCommand request = new()
        {
            EventId = 2,
            UserDiscordId = "1781398562"
        };
        var url = $"api/v1/EventUser";

        var client = _factory.CreateClient();

        var body = JsonSerializer.Serialize(request);
        var buffer = System.Text.Encoding.UTF8.GetBytes(body);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await client.PostAsync("api/v1/Event", byteContent);

        Assert.True(response.IsSuccessStatusCode);
        Assert.True(response.IsSuccessStatusCode);
    }
}
