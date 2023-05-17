using MassTransit;
using Saga.Choreography.Rockets.Consumers;
using Saga.Choreography.Shared;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Rockets;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<RocketBuiltEvent>(new Uri(Constants.LaunchQueue));
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.FailedLaunchQueue, e => e.ConfigureConsumer<LaunchFailedConsumer>(context));
    }
}