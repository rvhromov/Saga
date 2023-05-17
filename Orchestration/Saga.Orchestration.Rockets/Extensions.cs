using MassTransit;
using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Rockets.Database;
using Saga.Orchestration.Rockets.Rockets;
using Saga.Orchestration.Shared.Settings;
using Saga.Orchestration.Shared;

namespace Saga.Orchestration.Rockets;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RocketDbContext>(context => context.UseSqlite(configuration.GetConnectionString("RocketDb")));
        services.AddSettings(configuration);
        services.AddSystemServices();
        services.AddBus();

        return services;
    }

    private static void AddSystemServices(this IServiceCollection services)
    {
        services.AddScoped<IRocketService, RocketService>();
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