using Microsoft.EntityFrameworkCore;
using Saga.Choreography.Rockets.Rockets;

namespace Saga.Choreography.Rockets.Database;

internal sealed class RocketDbContext : DbContext
{
    public RocketDbContext(DbContextOptions<RocketDbContext> options) : base(options)
    {
    }

    public DbSet<Rocket> Rockets { get; set; }
}