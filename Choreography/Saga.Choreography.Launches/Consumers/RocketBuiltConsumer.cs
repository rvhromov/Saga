using MassTransit;
using Saga.Choreography.Launches.Launches;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Launches.Consumers;

public sealed class RocketBuiltConsumer : IConsumer<RocketBuiltEvent>
{
    private readonly ILaunchService _launchService;
    private readonly IPublishEndpoint _publishEndpoint;

    public RocketBuiltConsumer(ILaunchService launchService, IPublishEndpoint publishEndpoint)
    {
        _launchService = launchService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<RocketBuiltEvent> context)
    {
        try
        {
            await _launchService.LaunchRocketAsync(context.Message.RocketId);

            // To test Failure Path comment line above and uncomment line below

            // throw new Exception("Engines overheated!");
        }
        catch (Exception ex)
        {
            await _publishEndpoint.Publish(new LaunchFailedEvent(context.Message.RocketId, ex.Message));
        }
    }
}