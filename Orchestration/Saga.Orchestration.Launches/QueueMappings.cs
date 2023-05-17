using MassTransit;
using Saga.Orchestration.Launches.Consumers;
using Saga.Orchestration.Shared;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Launches;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<LaunchFailedEvent>(new Uri(Constants.FailedLaunchQueue));
        EndpointConvention.Map<RocketLaunchedEvent>(new Uri(Constants.SuccessfulLaunchQueue));
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.LaunchReceiveQueue, e => e.ConfigureConsumer<LaunchRocketConsumer>(context));
    }
}