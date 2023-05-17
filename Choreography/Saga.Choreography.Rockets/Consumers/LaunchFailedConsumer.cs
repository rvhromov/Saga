using MassTransit;
using Saga.Choreography.Rockets.Rockets;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Rockets.Consumers;

public sealed class LaunchFailedConsumer : IConsumer<LaunchFailedEvent>
{
    private readonly IRocketService _rocketService;

    public LaunchFailedConsumer(IRocketService rocketService) =>
        _rocketService = rocketService;

    public async Task Consume(ConsumeContext<LaunchFailedEvent> context)
    {
        await _rocketService.RemoveRocketAsync(context.Message.RocketId);
    }
}