using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saga.Orchestration.Orchestrator.States;

namespace Saga.Orchestration.Orchestrator.Database;

public sealed class MissionStateConfig : SagaClassMap<MissionState>
{
    protected override void Configure(EntityTypeBuilder<MissionState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);
    }
}