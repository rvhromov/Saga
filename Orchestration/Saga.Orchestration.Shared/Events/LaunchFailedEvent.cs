namespace Saga.Orchestration.Shared.Events;

public sealed record LaunchFailedEvent(Guid RocketId, string FailureMessage);
