using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Orchestration.Shared.Settings;
using Saga.Orchestration.Shared;
using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Launches.Database;
using Saga.Orchestration.Launches.Launches;

namespace Saga.Orchestration.Launches;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LaunchDbContext>(context => context.UseSqlite(configuration.GetConnectionString("LaunchDb")));
        services.AddSettings(configuration);
        services.AddSystemServices();
        services.AddBus();

        return services;
    }

    private static void AddSystemServices(this IServiceCollection services)
    {
        services.AddScoped<ILaunchService, LaunchService>();
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