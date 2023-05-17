using MassTransit;

namespace Saga.Orchestration.Orchestrator.States;

public sealed class MissionState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid RocketId { get; set; }
    public Guid? LaunchId { get; set; }
    public DateTime? RocketBuiltAt { get; set; }
    public DateTime? LaunchedAt { get; set; }
    public bool MissionFailed { get; set; }
    public bool MonitoringFailed { get; set; }
}