namespace Saga.Choreography.Shared.Events;

public sealed record RocketLaunchedEvent(Guid RocketId, Guid LaunchId);
