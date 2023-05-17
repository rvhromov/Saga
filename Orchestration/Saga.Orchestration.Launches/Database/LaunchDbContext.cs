using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Launches.Launches;

namespace Saga.Orchestration.Launches.Database;

internal sealed class LaunchDbContext : DbContext
{
    public LaunchDbContext(DbContextOptions<LaunchDbContext> options) : base(options)
    {
    }

    public DbSet<Launch> Launches { get; set; }
}
