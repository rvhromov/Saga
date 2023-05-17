using MassTransit;
using Saga.Choreography.Launches.Consumers;
using Saga.Choreography.Shared;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Launches;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<RocketLaunchedEvent>(new Uri(Constants.MonitoringQueue));
        EndpointConvention.Map<LaunchFailedEvent>(new Uri(Constants.FailedLaunchQueue));
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.LaunchQueue, e => e.ConfigureConsumer<RocketBuiltConsumer>(context));
    }
}