using Bot.Application.Reward.Commands;
using Bot.IntegrationTests.Commons;
using System.Net;
using Bot.Infrastructure.Persistence;

namespace Bot.IntegrationTests;

public class RewardControllerTest : ClientFixture
{
    public RewardControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}

    [Fact]
    public async Task ClaimReward_Should_Return200Ok()
    {
        ClaimRewardCommand reward = new()
        {
            FkEvent = 1001,
            UserId = "422315553620268941332"
        };

        var response = await AsPostAsync("api/v1/Reward/claim-reward", reward);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
