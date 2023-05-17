using MassTransit;
using Microsoft.Extensions.Logging;
using Saga.Orchestration.Shared.Commands;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Orchestrator.States;

public sealed class MissionStateMachine : MassTransitStateMachine<MissionState>
{
    public MissionStateMachine(ILogger<MissionStateMachine> logger)
    {
        ConfigureCorrelationIds();

        InstanceState(x => x.CurrentState);

        Initially(
            When(RocketBuilt)
            .Then(_ => logger.LogInformation("Rocket has been built."))
            .Then(context =>
            {
                context.Saga.RocketId = context.Message.RocketId;
                context.Saga.RocketBuiltAt = DateTime.UtcNow;
            })
            .Send(context => new LaunchRocketCommand(context.Saga.CorrelationId))
            .TransitionTo(RocketReady));

        During(RocketReady,
            When(LaunchFailed)
            .Then(context => logger.LogError($"Launch failed. Reason: {context.Message.FailureMessage}"))
            .Then(context => context.Saga.MissionFailed = true)
            .Send(context => new RemoveRocketCommand(context.Message.RocketId))
            .TransitionTo(MissionFailed));

        During(RocketReady,
            When(RocketLaunched)
            .Then(_ => logger.LogInformation("Rocket has been launched successfully."))
            .Then(context =>
            {
                context.Saga.RocketId = context.Message.RocketId;
                context.Saga.LaunchId = context.Message.LaunchId;
                context.Saga.LaunchedAt = DateTime.UtcNow;
            })
            .Send(context => new StartMissionMonitoringCommand(context.Message.RocketId, context.Message.LaunchId))
            .TransitionTo(OnMission));

        During(OnMission,
            When(MonitoringFailed)
            .Then(context => logger.LogWarning($"Monitoring failed. Reason: {context.Message.FailureMessage}"))
            .Then(context => context.Saga.MonitoringFailed = true)
            .TransitionTo(OnMonitoredMission));

        During(OnMission,
            When(MonitoringStarted)
            .Then(_ => logger.LogInformation("Mission monitoring has been started."))
            .TransitionTo(OnMonitoredMission));

        WhenEnter(OnMonitoredMission, x => x
            .IfElse(
                @if => @if.Saga.MonitoringFailed,
                then => then.Then(_ => logger.LogInformation("Mission completed with failed monitoring systems!")),
                @else => @else.Then(_ => logger.LogInformation("Mission completed without any issue!")))
            .Finalize());

        // If you want to remove a row from db after transaction is completed - uncomment line below
        // SetCompletedWhenFinalized();
    }

    public State RocketReady { get; }
    public State MissionFailed { get; }
    public State OnMission { get; }
    public State OnMonitoredMission { get; }

    public Event<RocketBuiltEvent> RocketBuilt { get; }
    public Event<LaunchFailedEvent> LaunchFailed { get; }
    public Event<RocketLaunchedEvent> RocketLaunched { get; }
    public Event<MonitoringFailedEvent> MonitoringFailed { get; }
    public Event<MonitoringStartedEvent> MonitoringStarted { get; }

    private void ConfigureCorrelationIds()
    {
        Event(() => RocketBuilt, x => x.CorrelateById(context => context.Message.RocketId));
        Event(() => LaunchFailed, x => x.CorrelateById(context => context.Message.RocketId));
        Event(() => RocketLaunched, x => x.CorrelateById(context => context.Message.RocketId));
        Event(() => MonitoringFailed, x => x.CorrelateById(context => context.Message.RocketId));
        Event(() => MonitoringStarted, x => x.CorrelateById(context => context.Message.RocketId));
    }
}