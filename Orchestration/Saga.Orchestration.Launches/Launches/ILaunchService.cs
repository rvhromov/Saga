namespace Saga.Orchestration.Launches.Launches;

public interface ILaunchService
{
    Task<Guid> LaunchRocketAsync(Guid rocketId);
}
