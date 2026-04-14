using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Playlists_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "playlists");

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "playlists",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                content = table.Column<string>(type: "text", nullable: false),
                occured_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                processed_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_outbox_messages", x => x.id));

        migrationBuilder.CreateTable(
            name: "playlists",
            schema: "playlists",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                cover_metadata_width = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_height = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                cover_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                cover_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                is_auto_generated = table.Column<bool>(type: "boolean", nullable: false),
                is_public = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_playlists", x => x.id));

        migrationBuilder.CreateTable(
            name: "playlist_collaborators",
            schema: "playlists",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                playlist_id = table.Column<Guid>(type: "uuid", nullable: false),
                collaborator_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_playlist_collaborators", x => x.id);
                table.ForeignKey(
                    name: "FK_playlist_collaborators_playlists_playlist_id",
                    column: x => x.playlist_id,
                    principalSchema: "playlists",
                    principalTable: "playlists",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "playlist_tracks",
            schema: "playlists",
            columns: table => new
            {
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                playlist_id = table.Column<Guid>(type: "uuid", nullable: false),
                position = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_playlist_tracks", x => new { x.playlist_id, x.track_id });
                table.ForeignKey(
                    name: "FK_playlist_tracks_playlists_playlist_id",
                    column: x => x.playlist_id,
                    principalSchema: "playlists",
                    principalTable: "playlists",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_outbox_messages_processed_on",
            schema: "playlists",
            table: "outbox_messages",
            column: "processed_on");

        migrationBuilder.CreateIndex(
            name: "IX_playlist_collaborators_playlist_id_collaborator_id",
            schema: "playlists",
            table: "playlist_collaborators",
            columns: new[] { "playlist_id", "collaborator_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_playlist_tracks_playlist_id_position",
            schema: "playlists",
            table: "playlist_tracks",
            columns: new[] { "playlist_id", "position" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "playlists");

        migrationBuilder.DropTable(
            name: "playlist_collaborators",
            schema: "playlists");

        migrationBuilder.DropTable(
            name: "playlist_tracks",
            schema: "playlists");

        migrationBuilder.DropTable(
            name: "playlists",
            schema: "playlists");
    }
}
