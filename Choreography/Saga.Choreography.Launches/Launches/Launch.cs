namespace Saga.Choreography.Launches.Launches;

public class Launch
{
    public Guid Id { get; set; }
    public DateTime LaunchAt{ get; set; }
    public Guid RocketId { get; set; }
}