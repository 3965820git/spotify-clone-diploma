using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Playlists_Add_Explicit_PlaylistId_Property_To_PlaylistTrack : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
        => migrationBuilder.DropIndex(
            name: "IX_playlist_tracks_track_id_position_playlist_id",
            schema: "playlists",
            table: "playlist_tracks");

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.CreateIndex(
            name: "IX_playlist_tracks_track_id_position_playlist_id",
            schema: "playlists",
            table: "playlist_tracks",
            columns: new[] { "track_id", "position", "playlist_id" },
            unique: true);
}
