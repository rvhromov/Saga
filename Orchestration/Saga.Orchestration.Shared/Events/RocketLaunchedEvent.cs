namespace Saga.Orchestration.Shared.Events;

public sealed record RocketLaunchedEvent(Guid RocketId, Guid LaunchId);
