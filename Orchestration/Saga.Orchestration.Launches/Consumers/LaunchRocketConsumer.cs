using MassTransit;
using Saga.Orchestration.Launches.Launches;
using Saga.Orchestration.Shared.Commands;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Launches.Consumers;

public sealed class LaunchRocketConsumer : IConsumer<LaunchRocketCommand>
{
	private readonly IPublishEndpoint _publishEndpoint;
	private readonly ILaunchService _launchService;

    public LaunchRocketConsumer(IPublishEndpoint publishEndpoint, ILaunchService launchService)
    {
        _publishEndpoint = publishEndpoint;
        _launchService = launchService;
    }

    public async Task Consume(ConsumeContext<LaunchRocketCommand> context)
    {
		try
		{
            await _launchService.LaunchRocketAsync(context.Message.RocketId);

            // To test Failure Path comment line above and uncomment line below

            //throw new Exception("Engines overheated!");
        }
        catch (Exception ex)
		{
            await _publishEndpoint.Publish(new LaunchFailedEvent(context.Message.RocketId, ex.Message));
        }
    }
}