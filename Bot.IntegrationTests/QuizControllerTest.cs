using Microsoft.AspNetCore.Mvc.Testing;

namespace Bot.IntegrationTests;

public class QuizControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public QuizControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/api/v1/Quiz/get-by-event-id=1")]
    public async Task GetQuizByEventIdEndpoint_Should_ReturnOk(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }
}
