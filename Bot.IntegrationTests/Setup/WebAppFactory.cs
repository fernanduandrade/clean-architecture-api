using Bot.IntegrationTests.SeedWork;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class WebAppTestFactory<TProgram, TDbContext> :
    WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class where TDbContext : DbContext
{
    private readonly TestcontainerDatabase _container =
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "he4rtbot",
                Password = "supersecuritypassword",
                Username = "postgres"
            })
            .WithImage("postgres:11")
            .WithCleanUp(true)
            .Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault( d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null) services.Remove(descriptor);
            services.AddDbContext<TDbContext>(options => { options.UseNpgsql(_container.ConnectionString); });
            

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<TDbContext>();
            context.Database.EnsureCreated();
            
            services.AddTransient<SeedCreator>();
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}
