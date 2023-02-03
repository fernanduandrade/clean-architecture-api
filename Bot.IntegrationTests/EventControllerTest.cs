using Bot.Application.Event.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Bot.IntegrationTests;

public class EventControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EventControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("api/v1/Event?PageNumber=1&PageSize=1")]
    public async Task GetEventPaginated_Should_Return200Ok(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);
        Assert.True(response.IsSuccessStatusCode);
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

        var client = _factory.CreateClient();
        var body = JsonSerializer.Serialize(@event);
        var buffer = System.Text.Encoding.UTF8.GetBytes(body);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await client.PostAsync("api/v1/Event", byteContent);

        Assert.True(response.IsSuccessStatusCode);
    }
}
