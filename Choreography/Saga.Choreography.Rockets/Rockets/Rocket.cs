namespace Saga.Choreography.Rockets.Rockets;

public class Rocket
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Destination { get; set; }
    public DateTime CreatedAt { get; set; }
}