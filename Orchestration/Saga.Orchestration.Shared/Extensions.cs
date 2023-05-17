using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Saga.Orchestration.Shared.Settings;

namespace Saga.Orchestration.Shared;

public static class Extensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection(nameof(MessageBrokerSettings)));
        services.AddSingleton(p => p.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        return services;
    }
}