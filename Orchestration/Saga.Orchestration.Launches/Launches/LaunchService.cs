using MassTransit;
using Saga.Orchestration.Launches.Database;
using Saga.Orchestration.Shared.Events;

namespace Saga.Orchestration.Launches.Launches;

internal sealed class LaunchService : ILaunchService
{
    private readonly LaunchDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public LaunchService(LaunchDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> LaunchRocketAsync(Guid rocketId)
    {
        var launch = new Launch
        {
            Id = Guid.NewGuid(),
            RocketId = rocketId,
            LaunchAt = DateTime.UtcNow
        };

        _dbContext.Launches.Add(launch);
        await _dbContext.SaveChangesAsync();

        await _publishEndpoint.Publish(new RocketLaunchedEvent(rocketId, launch.Id));

        return launch.Id;
    }
}
