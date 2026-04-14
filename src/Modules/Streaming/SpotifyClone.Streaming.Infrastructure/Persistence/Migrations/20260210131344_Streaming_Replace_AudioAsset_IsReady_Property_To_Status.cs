using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Replace_AudioAsset_IsReady_Property_To_Status : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "is_ready",
            schema: "streaming",
            table: "audio_assets");

        migrationBuilder.AddColumn<string>(
            name: "status",
            schema: "streaming",
            table: "audio_assets",
            type: "text",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "status",
            schema: "streaming",
            table: "audio_assets");

        migrationBuilder.AddColumn<bool>(
            name: "is_ready",
            schema: "streaming",
            table: "audio_assets",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }
}
