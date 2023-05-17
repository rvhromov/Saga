﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saga.Choreography.Launches.Database;

#nullable disable

namespace Saga.Choreography.Launches.Database.Migrations
{
    [DbContext(typeof(LaunchDbContext))]
    [Migration("20230503080641_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Saga.Choreography.Launches.Launches.Launch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LaunchAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RocketId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Launches");
                });
#pragma warning restore 612, 618
        }
    }
}
