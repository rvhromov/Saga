using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Choreography.Shared;
using Saga.Choreography.Shared.Settings;

namespace Saga.Choreography.Monitoring;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddBus();

        return services;
    }

    private static void AddBus(this IServiceCollection services)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.UseRabbitMq();
            busConfig.AddConsumers(typeof(Program).Assembly);

            QueueMappings.MapEventsToQueues();
        });
    }

    private static void UseRabbitMq(this IBusRegistrationConfigurator busConfig)
    {
        busConfig.UsingRabbitMq((context, rabbitConfigurator) =>
        {
            var settings = context.GetRequiredService<MessageBrokerSettings>();

            rabbitConfigurator.Host(settings.Host, h =>
            {
                h.Username(settings.Username);
                h.Password(settings.Password);
            });

            rabbitConfigurator.MapQueuesToReceiveEndpoints(context);
        });
    }
}