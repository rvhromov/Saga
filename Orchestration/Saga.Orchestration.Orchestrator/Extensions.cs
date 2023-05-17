using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saga.Orchestration.Orchestrator.Database;
using Saga.Orchestration.Orchestrator.States;
using Saga.Orchestration.Shared;
using Saga.Orchestration.Shared.Settings;

namespace Saga.Orchestration.Orchestrator;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddBus(configuration);

        return services;
    }

    private static void AddBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.UseRabbitMq();
            busConfig.AddConsumers(typeof(Program).Assembly);

            busConfig
                .AddSagaStateMachine<MissionStateMachine, MissionState>()
                .EntityFrameworkRepository(c =>
                {
                    c.ConcurrencyMode = ConcurrencyMode.Optimistic;
                    c.AddDbContext<DbContext, OrchestrationDbContext>((_, opt) => opt.UseSqlite(configuration.GetConnectionString("StateDb")));
                });

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

            rabbitConfigurator.ReceiveEndpoint(ec => ec.StateMachineSaga<MissionState>(context));
        });
    }
}