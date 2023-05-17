using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saga.Orchestration.Orchestrator.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionState",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrentState = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    RocketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LaunchId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RocketBuiltAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LaunchedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MissionFailed = table.Column<bool>(type: "INTEGER", nullable: false),
                    MonitoringFailed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionState", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionState");
        }
    }
}
