using MassTransit;
using Microsoft.EntityFrameworkCore;
using Saga.Choreography.Rockets.Database;
using Saga.Choreography.Shared.Events;

namespace Saga.Choreography.Rockets.Rockets;

internal sealed class RocketService : IRocketService
{
    private readonly RocketDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public RocketService(RocketDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> BuildRocketAsync(NewMissionRequest request)
    {
        var rocket = new Rocket
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Destination = request.Destination,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Rockets.Add(rocket);
        await _dbContext.SaveChangesAsync();

        await _publishEndpoint.Publish(new RocketBuiltEvent(rocket.Id));

        return rocket.Id;
    }

    public async Task RemoveRocketAsync(Guid id)
    {
        var rocket = await _dbContext.Rockets.FirstOrDefaultAsync(r => r.Id == id);

        if (rocket is null)
        {
            return;
        }

        _dbContext.Rockets.Remove(rocket);
        await _dbContext.SaveChangesAsync();
    }
}