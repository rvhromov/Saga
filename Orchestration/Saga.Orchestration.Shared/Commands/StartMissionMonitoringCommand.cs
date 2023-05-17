namespace Saga.Orchestration.Shared.Commands;

public sealed record StartMissionMonitoringCommand(Guid RocketId, Guid LaunchId);