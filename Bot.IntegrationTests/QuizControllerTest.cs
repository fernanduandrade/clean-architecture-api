using System.Net;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.Commons;
using Bot.IntegrationTests.Setup;

namespace Bot.IntegrationTests;

public class QuizControllerTest : ClientFixture
{
    public QuizControllerTest(WebAppTestFactory<Program, AppDbContext> factory) : base(factory) {}

    [Theory]
    [InlineData("api/v1/Quiz/get-by-event-id?EventId=1")]
    public async Task GetQuizByEventIdEndpoint_Should_Return200Ok(string url)
    {
        await SeedWork.AddEvents();
        var response = await AsGetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
