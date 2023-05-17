using MassTransit;
using Saga.Orchestration.Rockets.Rockets;
using Saga.Orchestration.Shared.Commands;

namespace Saga.Orchestration.Rockets.Consumers.cs;

public sealed class RemoveRocketConsumer : IConsumer<RemoveRocketCommand>
{
    private readonly IRocketService _rocketService;

    public RemoveRocketConsumer(IRocketService rocketService) =>
        _rocketService = rocketService;

    public async Task Consume(ConsumeContext<RemoveRocketCommand> context)
    {
        await _rocketService.RemoveRocketAsync(context.Message.RocketId);
    }
}