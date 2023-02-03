using System.Net.Http.Headers;
using Bot.Application.Reward.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace Bot.IntegrationTests;

public class RewardControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RewardControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateReward_Should_Return200Ok()
    {
        ClaimRewardCommand reward = new()
        {
            FkEvent = 1,
            UserId = "415553620268941332"
        };

        var client = _factory.CreateClient();
        var body = JsonSerializer.Serialize(reward);
        var buffer = System.Text.Encoding.UTF8.GetBytes(body);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await client.PostAsync("api/v1/Reward/claim-reward", byteContent);

        Assert.True(response.IsSuccessStatusCode);
    }
}
