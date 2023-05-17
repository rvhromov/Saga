namespace Saga.Choreography.Shared.Events;

public sealed record MonitoringFailedEvent(Guid LaunchId, Guid RocketId, string FailureMessage);
