using Bot.Application.Common.Interfaces;
using Bot.Infrastructure.Persistence;
using Bot.Presentation.Configuration;
using Bot.Presentation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigSwagger>();
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;

        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v' VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}