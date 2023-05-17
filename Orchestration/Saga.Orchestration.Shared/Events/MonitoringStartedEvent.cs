namespace Saga.Orchestration.Shared.Events;

public sealed record MonitoringStartedEvent(Guid LaunchId, Guid RocketId);
