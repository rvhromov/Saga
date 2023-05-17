namespace Saga.Choreography.Shared.Events;

public sealed record LaunchFailedEvent(Guid RocketId, string FailureMessage);
