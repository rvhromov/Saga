using MassTransit;
using Saga.Orchestration.Rockets.Consumers.cs;
using Saga.Orchestration.Shared;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Rockets;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<RocketBuiltEvent>(new Uri(Constants.RocketReadyQueue));
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.RemoveRocketReceiveQueue, e => e.ConfigureConsumer<RemoveRocketConsumer>(context));
    }
}