using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Playlists_Add_CreatorId_Property_To_Playlist : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_playlist_tracks",
            schema: "playlists",
            table: "playlist_tracks");

        migrationBuilder.AddColumn<Guid>(
            name: "creator_id",
            schema: "playlists",
            table: "playlist_tracks",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddPrimaryKey(
            name: "PK_playlist_tracks",
            schema: "playlists",
            table: "playlist_tracks",
            column: "track_id");

        migrationBuilder.CreateIndex(
            name: "IX_playlist_tracks_track_id_playlist_id",
            schema: "playlists",
            table: "playlist_tracks",
            columns: new[] { "track_id", "playlist_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_playlist_tracks_track_id_position_playlist_id",
            schema: "playlists",
            table: "playlist_tracks",
            columns: new[] { "track_id", "position", "playlist_id" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_playlist_tracks",
            schema: "playlists",
            table: "playlist_tracks");

        migrationBuilder.DropIndex(
            name: "IX_playlist_tracks_track_id_playlist_id",
            schema: "playlists",
            table: "playlist_tracks");

        migrationBuilder.DropIndex(
            name: "IX_playlist_tracks_track_id_position_playlist_id",
            schema: "playlists",
            table: "playlist_tracks");

        migrationBuilder.DropColumn(
            name: "creator_id",
            schema: "playlists",
            table: "playlist_tracks");

        migrationBuilder.AddPrimaryKey(
            name: "PK_playlist_tracks",
            schema: "playlists",
            table: "playlist_tracks",
            columns: new[] { "playlist_id", "track_id" });
    }
}
