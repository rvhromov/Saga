using MassTransit;
using Microsoft.Extensions.Logging;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Monitoring.Consumers;

public sealed class RocketLaunchedConsumer : IConsumer<RocketLaunchedEvent>
{
    private readonly ILogger<RocketLaunchedConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public RocketLaunchedConsumer(ILogger<RocketLaunchedConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<RocketLaunchedEvent> context)
    {
        try
        {
            _logger.LogInformation(
                $"Successful launch {context.Message.LaunchId}. " +
                $"Monitoring of {context.Message.RocketId} rocket has been started.");

            // To test Failure Path comment above line and uncomment line below
            // Currently no one subscribed to MonitoringFailedEvent because this failure doesn't have a strong impact on the transaction

            //throw new Exception("Signal has been lost.");
        }
        catch (Exception ex)
        {
            await _publishEndpoint.Publish(new MonitoringFailedEvent(context.Message.LaunchId, context.Message.RocketId, ex.Message));
        }
    }
}