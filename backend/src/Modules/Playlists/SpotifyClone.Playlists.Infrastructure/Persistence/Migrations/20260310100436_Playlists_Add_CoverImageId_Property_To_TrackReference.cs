using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Playlists_Add_CoverImageId_Property_To_TrackReference : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
        => migrationBuilder.AddColumn<Guid>(
            name: "cover_image_id",
            schema: "playlists",
            table: "track_references",
            type: "uuid",
            nullable: true);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.DropColumn(
            name: "cover_image_id",
            schema: "playlists",
            table: "track_references");
}
