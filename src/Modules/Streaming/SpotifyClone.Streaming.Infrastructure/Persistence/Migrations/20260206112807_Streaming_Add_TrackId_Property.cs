using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Add_TrackId_Property : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "streaming");

        migrationBuilder.CreateTable(
            name: "audio_assets",
            schema: "streaming",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                format = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                is_ready = table.Column<bool>(type: "boolean", nullable: false),
                created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                track_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_audio_assets", x => x.id));

        migrationBuilder.CreateTable(
            name: "image_assets",
            schema: "streaming",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                metadata_width = table.Column<int>(type: "integer", nullable: true),
                metadata_height = table.Column<int>(type: "integer", nullable: true),
                metadata_file_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                is_ready = table.Column<bool>(type: "boolean", nullable: false),
                created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_image_assets", x => x.id));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "audio_assets",
            schema: "streaming");

        migrationBuilder.DropTable(
            name: "image_assets",
            schema: "streaming");
    }
}
