using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Saga.Choreography.Shared.Settings;

namespace Saga.Choreography.Shared;

public static class Extensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection(nameof(MessageBrokerSettings)));
        services.AddSingleton(p => p.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        return services;
    }
}
