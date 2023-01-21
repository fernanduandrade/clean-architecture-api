using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bot.Presentation.Configuration;

public class ConfigSwagger : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigSwagger(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "He4rtBot - API",
            Version = description.ApiVersion.ToString(),
            Description = "Docs para api do bot da he4rt",
            Contact = new OpenApiContact() { Email = "onanduandrade@gmail.com", Name = "Desenvolvedor" },
            TermsOfService = new Uri("https://opensource.org/license/MIT"),
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
        };
        if (description.IsDeprecated) info.Description += "Versão obsoleta";

        return info;
    }
}

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerDefaultValues>();
        });
    }

    public static void UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }

        });
    }
}

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation opeation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        opeation.Deprecated = apiDescription.IsDeprecated();

        if (opeation.Parameters is null) return;
        foreach (var parameter in opeation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
            parameter.Description ??= description.ModelMetadata.Description;
            parameter.Required |= description.IsRequired;
        }
    }
}