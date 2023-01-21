using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetEntryAssembly());
            return services;
        }
    }
}
