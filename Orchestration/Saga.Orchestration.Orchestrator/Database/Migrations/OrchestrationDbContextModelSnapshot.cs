﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saga.Orchestration.Orchestrator.Database;

#nullable disable

namespace Saga.Orchestration.Orchestrator.Database.Migrations
{
    [DbContext(typeof(OrchestrationDbContext))]
    partial class OrchestrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Saga.Orchestration.Orchestrator.States.MissionState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentState")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LaunchId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LaunchedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("MissionFailed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MonitoringFailed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RocketBuiltAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RocketId")
                        .HasColumnType("TEXT");

                    b.HasKey("CorrelationId");

                    b.ToTable("MissionState");
                });
#pragma warning restore 612, 618
        }
    }
}