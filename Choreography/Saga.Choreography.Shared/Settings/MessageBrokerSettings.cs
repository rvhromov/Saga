namespace Saga.Choreography.Shared.Settings;

public sealed record MessageBrokerSettings
{
    public string Host { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}