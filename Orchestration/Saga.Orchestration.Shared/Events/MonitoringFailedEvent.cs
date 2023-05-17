namespace Saga.Orchestration.Shared.Events;

public sealed record MonitoringFailedEvent(Guid LaunchId, Guid RocketId, string FailureMessage);
