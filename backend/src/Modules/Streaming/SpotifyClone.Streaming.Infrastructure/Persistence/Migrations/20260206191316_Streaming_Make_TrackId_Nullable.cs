using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Make_TrackId_Nullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
        => migrationBuilder.AlterColumn<Guid>(
            name: "track_id",
            schema: "streaming",
            table: "audio_assets",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.AlterColumn<Guid>(
            name: "track_id",
            schema: "streaming",
            table: "audio_assets",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);
}
