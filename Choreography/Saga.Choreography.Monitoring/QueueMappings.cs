using MassTransit;
using Saga.Choreography.Monitoring.Consumers;
using Saga.Choreography.Shared;

namespace Saga.Choreography.Monitoring;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.MonitoringQueue, e => e.ConfigureConsumer<RocketLaunchedConsumer>(context));
    }
}
