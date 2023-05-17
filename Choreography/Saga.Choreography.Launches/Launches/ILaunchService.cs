namespace Saga.Choreography.Launches.Launches;

public interface ILaunchService
{
    Task<Guid> LaunchRocketAsync(Guid rocketId);
}