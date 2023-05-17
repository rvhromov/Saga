using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Saga.Orchestration.Orchestrator.Database;

internal sealed class OrchestrationDbContext : SagaDbContext
{
    public OrchestrationDbContext(DbContextOptions<OrchestrationDbContext> options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new MissionStateConfig();
        }
    }
}