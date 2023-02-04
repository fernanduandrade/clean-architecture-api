using System.Net.Http.Headers;
using Bot.Application.Reward.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Bot.IntegrationTests.Commons;
using System.Net;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.Setup;

namespace Bot.IntegrationTests;

public class RewardControllerTest : ClientFixture
{

    public RewardControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}

    [Fact]
    public async Task CreateReward_Should_Return200Ok()
    {
        ClaimRewardCommand reward = new()
        {
            FkEvent = 1,
            UserId = "415553620268941332"
        };

        var response = await AsPostAsync("api/v1/Reward/claim-reward", reward);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
