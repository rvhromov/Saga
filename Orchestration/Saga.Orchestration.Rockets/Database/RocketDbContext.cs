using Microsoft.EntityFrameworkCore;
using Saga.Orchestration.Rockets.Rockets;

namespace Saga.Orchestration.Rockets.Database;

internal sealed class RocketDbContext : DbContext
{
    public RocketDbContext(DbContextOptions<RocketDbContext> options) : base(options)
    {
    }

    public DbSet<Rocket> Rockets { get; set; }
}