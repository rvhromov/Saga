using MassTransit;
using Saga.Orchestration.Shared;
using Saga.Orchestration.Shared.Commands;

namespace Saga.Orchestration.Orchestrator;

public static class QueueMappings
{
    public static void MapEventsToQueues()
    {
        EndpointConvention.Map<LaunchRocketCommand>(new Uri(Constants.LaunchQueue));
        EndpointConvention.Map<RemoveRocketCommand>(new Uri(Constants.RemoveRocketQueue));
        EndpointConvention.Map<StartMissionMonitoringCommand>(new Uri(Constants.MonitoringQueue));
    }
}