using MassTransit;
using Microsoft.Extensions.Logging;
using Saga.Orchestration.Shared.Commands;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Monitoring.Consumers;

public sealed class StartMissionMonitoringConsumer : IConsumer<StartMissionMonitoringCommand>
{
    private readonly ILogger<StartMissionMonitoringConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public StartMissionMonitoringConsumer(ILogger<StartMissionMonitoringConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<StartMissionMonitoringCommand> context)
    {
        try
        {
            _logger.LogInformation(
                $"Successful launch {context.Message.LaunchId}. " +
                $"Monitoring of {context.Message.RocketId} rocket has been started.");

            await _publishEndpoint.Publish(new MonitoringStartedEvent(context.Message.LaunchId, context.Message.RocketId));

            // To test Failure Path comment above line and uncomment line below
            // Currently only Orchestrator subscribed to MonitoringFailedEvent, however no further action is taken
            // because this failure doesn't have a strong impact on the transaction

            //throw new Exception("Signal has been lost.");
        }
        catch (Exception ex)
        {
            await _publishEndpoint.Publish(new MonitoringFailedEvent(context.Message.LaunchId, context.Message.RocketId, ex.Message));
        }
    }
}