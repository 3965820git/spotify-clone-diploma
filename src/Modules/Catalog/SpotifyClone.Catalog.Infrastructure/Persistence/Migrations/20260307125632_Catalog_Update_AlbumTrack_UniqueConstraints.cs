using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Catalog_Update_AlbumTrack_UniqueConstraints : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_album_tracks_album_id_position",
            schema: "catalog",
            table: "album_tracks");

        migrationBuilder.CreateIndex(
            name: "IX_album_tracks_track_id_album_id",
            schema: "catalog",
            table: "album_tracks",
            columns: new[] { "track_id", "album_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_album_tracks_track_id_position_album_id",
            schema: "catalog",
            table: "album_tracks",
            columns: new[] { "track_id", "position", "album_id" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_album_tracks_track_id_album_id",
            schema: "catalog",
            table: "album_tracks");

        migrationBuilder.DropIndex(
            name: "IX_album_tracks_track_id_position_album_id",
            schema: "catalog",
            table: "album_tracks");

        migrationBuilder.CreateIndex(
            name: "IX_album_tracks_album_id_position",
            schema: "catalog",
            table: "album_tracks",
            columns: new[] { "album_id", "position" },
            unique: true);
    }
}
