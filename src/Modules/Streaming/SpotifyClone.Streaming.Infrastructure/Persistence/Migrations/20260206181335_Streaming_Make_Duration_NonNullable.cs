using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Make_Duration_NonNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
        => migrationBuilder.AlterColumn<TimeSpan>(
            name: "duration",
            schema: "streaming",
            table: "audio_assets",
            type: "interval",
            nullable: false,
            defaultValue: new TimeSpan(0, 0, 0, 0, 0),
            oldClrType: typeof(TimeSpan),
            oldType: "interval",
            oldNullable: true);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.AlterColumn<TimeSpan>(
            name: "duration",
            schema: "streaming",
            table: "audio_assets",
            type: "interval",
            nullable: true,
            oldClrType: typeof(TimeSpan),
            oldType: "interval");
}
