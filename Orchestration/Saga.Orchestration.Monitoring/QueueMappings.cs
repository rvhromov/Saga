using MassTransit;
using Saga.Orchestration.Monitoring.Consumers;
using Saga.Orchestration.Shared;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Monitoring;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<MonitoringFailedEvent>(new Uri(Constants.FailedMonitoringQueue));
        EndpointConvention.Map<MonitoringStartedEvent>(new Uri(Constants.SuccessfulMonitoringQueue));
    }

    public static void MapQueuesToReceiveEndpoints(
        this IRabbitMqBusFactoryConfigurator rabbitConfigurator,
        IBusRegistrationContext context)
    {
        rabbitConfigurator.ReceiveEndpoint(Constants.MonitoringReceiveQueue, e => e.ConfigureConsumer<StartMissionMonitoringConsumer>(context));
    }
}