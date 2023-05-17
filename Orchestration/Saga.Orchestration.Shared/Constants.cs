namespace Saga.Orchestration.Shared;

public static class Constants
{
    public const string RocketReadyQueue = "queue:rocket-ready-queue";

    public const string LaunchQueue = "queue:launch-queue";
    public const string LaunchReceiveQueue = "launch-queue";

    public const string FailedLaunchQueue = "queue:failed-launch-queue";

    public const string RemoveRocketQueue = "queue:remove-rocket-queue";
    public const string RemoveRocketReceiveQueue = "remove-rocket-queue";

    public const string SuccessfulLaunchQueue = "queue:success-launch-queue";

    public const string MonitoringQueue = "queue:monitoring-queue";
    public const string MonitoringReceiveQueue = "monitoring-queue";

    public const string FailedMonitoringQueue = "queue:failed-monitoring-queue";
    public const string SuccessfulMonitoringQueue = "queue:success-monitoring-queue";
}