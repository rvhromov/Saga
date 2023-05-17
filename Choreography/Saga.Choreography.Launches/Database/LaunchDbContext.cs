using Microsoft.EntityFrameworkCore;
using Saga.Choreography.Launches.Launches;

namespace Saga.Choreography.Launches.Database;

internal sealed class LaunchDbContext : DbContext
{
    public LaunchDbContext(DbContextOptions<LaunchDbContext> options) : base(options)
    {
    }

    public DbSet<Launch> Launches { get; set; }
}