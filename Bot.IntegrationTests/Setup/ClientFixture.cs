using System.Net.Http.Headers;
using System.Text.Json;
using Bot.Infrastructure.Persistence;
using Bot.IntegrationTests.SeedWork;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.IntegrationTests.Commons;

public class ClientFixture : IClassFixture<WebAppTestFactory<Program, AppDbContext>>
{
    private readonly WebAppTestFactory<Program, AppDbContext> Factory;
    public readonly HttpClient Client;
    public readonly SeedCreator SeedWork;
    public readonly AppDbContext dbContext;

    public ClientFixture(WebAppTestFactory<Program, AppDbContext> factory)
    {
        Factory = factory;
        var scope = factory.Services.CreateScope();
        dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Client = factory.CreateClient();
        SeedWork = scope.ServiceProvider.GetRequiredService<SeedCreator>();
    }

    public async Task<HttpResponseMessage> AsPostAsync<T>(string url, T body)
    {
        var json = JsonSerializer.Serialize(body);
        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return await Client.PostAsync(url, byteContent);
    }

    public async Task<HttpResponseMessage> AsPutAsync<T>(string url, T body)
    {
        var json = JsonSerializer.Serialize(body);
        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return await Client.PutAsync(url, byteContent);
    }

    public async Task<HttpResponseMessage> AsGetAsync(string url)
    {
        return await Client.GetAsync(url);
    }

    public async Task<HttpResponseMessage> AsDeleteAsync(string url)
    {   
        return await Client.DeleteAsync(url);
    }
}
