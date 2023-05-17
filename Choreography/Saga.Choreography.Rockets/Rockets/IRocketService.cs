namespace Saga.Choreography.Rockets.Rockets;

public interface IRocketService
{
    Task<Guid> BuildRocketAsync(NewMissionRequest request);
    Task RemoveRocketAsync(Guid id);
}
