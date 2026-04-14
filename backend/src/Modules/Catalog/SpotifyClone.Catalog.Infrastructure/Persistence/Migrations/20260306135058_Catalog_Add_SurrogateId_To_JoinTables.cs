using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Catalog_Add_SurrogateId_To_JoinTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "catalog");

        migrationBuilder.CreateTable(
            name: "albums",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                release_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                status = table.Column<string>(type: "text", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                cover_metadata_width = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_height = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                cover_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                cover_image_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_albums", x => x.id));

        migrationBuilder.CreateTable(
            name: "artists",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                bio = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "text", nullable: false),
                avatar_metadata_width = table.Column<int>(type: "integer", nullable: true),
                avatar_metadata_height = table.Column<int>(type: "integer", nullable: true),
                avatar_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                avatar_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                avatar_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                banner_metadata_width = table.Column<int>(type: "integer", nullable: true),
                banner_metadata_height = table.Column<int>(type: "integer", nullable: true),
                banner_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                banner_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                banner_image_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_artists", x => x.id));

        migrationBuilder.CreateTable(
            name: "genres",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                cover_metadata_width = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_height = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                cover_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                cover_image_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_genres", x => x.id));

        migrationBuilder.CreateTable(
            name: "moods",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                cover_metadata_width = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_height = table.Column<int>(type: "integer", nullable: true),
                cover_metadata_file_type = table.Column<string>(type: "text", nullable: true),
                cover_metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: true),
                cover_image_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_moods", x => x.id));

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "catalog",
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
            name: "tracks",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                release_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                contains_explicit_content = table.Column<bool>(type: "boolean", nullable: false),
                status = table.Column<string>(type: "text", nullable: false),
                audio_file_id = table.Column<Guid>(type: "uuid", nullable: true),
                album_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_tracks", x => x.id));

        migrationBuilder.CreateTable(
            name: "album_main_artists",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                album_id = table.Column<Guid>(type: "uuid", nullable: false),
                artist_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_album_main_artists", x => x.id);
                table.ForeignKey(
                    name: "FK_album_main_artists_albums_album_id",
                    column: x => x.album_id,
                    principalSchema: "catalog",
                    principalTable: "albums",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "album_tracks",
            schema: "catalog",
            columns: table => new
            {
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                album_id = table.Column<Guid>(type: "uuid", nullable: false),
                position = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_album_tracks", x => new { x.album_id, x.track_id });
                table.ForeignKey(
                    name: "FK_album_tracks_albums_album_id",
                    column: x => x.album_id,
                    principalSchema: "catalog",
                    principalTable: "albums",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "artist_gallery_images",
            schema: "catalog",
            columns: table => new
            {
                image_id = table.Column<Guid>(type: "uuid", nullable: false),
                artist_id = table.Column<Guid>(type: "uuid", nullable: false),
                metadata_width = table.Column<int>(type: "integer", nullable: false),
                metadata_height = table.Column<int>(type: "integer", nullable: false),
                metadata_file_type = table.Column<string>(type: "text", nullable: false),
                metadata_size_in_bytes = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_artist_gallery_images", x => new { x.artist_id, x.image_id });
                table.ForeignKey(
                    name: "FK_artist_gallery_images_artists_artist_id",
                    column: x => x.artist_id,
                    principalSchema: "catalog",
                    principalTable: "artists",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "track_featured_artists",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                artist_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_track_featured_artists", x => x.id);
                table.ForeignKey(
                    name: "FK_track_featured_artists_tracks_track_id",
                    column: x => x.track_id,
                    principalSchema: "catalog",
                    principalTable: "tracks",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "track_genres",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                genre_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_track_genres", x => x.id);
                table.ForeignKey(
                    name: "FK_track_genres_tracks_track_id",
                    column: x => x.track_id,
                    principalSchema: "catalog",
                    principalTable: "tracks",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "track_main_artists",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                artist_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_track_main_artists", x => x.id);
                table.ForeignKey(
                    name: "FK_track_main_artists_tracks_track_id",
                    column: x => x.track_id,
                    principalSchema: "catalog",
                    principalTable: "tracks",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "track_moods",
            schema: "catalog",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                track_id = table.Column<Guid>(type: "uuid", nullable: false),
                mood_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_track_moods", x => x.id);
                table.ForeignKey(
                    name: "FK_track_moods_tracks_track_id",
                    column: x => x.track_id,
                    principalSchema: "catalog",
                    principalTable: "tracks",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_album_main_artists_album_id_artist_id",
            schema: "catalog",
            table: "album_main_artists",
            columns: new[] { "album_id", "artist_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_album_tracks_album_id_position",
            schema: "catalog",
            table: "album_tracks",
            columns: new[] { "album_id", "position" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_albums_release_date",
            schema: "catalog",
            table: "albums",
            column: "release_date");

        migrationBuilder.CreateIndex(
            name: "IX_genres_name",
            schema: "catalog",
            table: "genres",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_moods_name",
            schema: "catalog",
            table: "moods",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_outbox_messages_processed_on",
            schema: "catalog",
            table: "outbox_messages",
            column: "processed_on");

        migrationBuilder.CreateIndex(
            name: "IX_track_featured_artists_track_id_artist_id",
            schema: "catalog",
            table: "track_featured_artists",
            columns: new[] { "track_id", "artist_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_track_genres_track_id_genre_id",
            schema: "catalog",
            table: "track_genres",
            columns: new[] { "track_id", "genre_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_track_main_artists_track_id_artist_id",
            schema: "catalog",
            table: "track_main_artists",
            columns: new[] { "track_id", "artist_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_track_moods_track_id_mood_id",
            schema: "catalog",
            table: "track_moods",
            columns: new[] { "track_id", "mood_id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_tracks_album_id",
            schema: "catalog",
            table: "tracks",
            column: "album_id");

        migrationBuilder.CreateIndex(
            name: "IX_tracks_audio_file_id",
            schema: "catalog",
            table: "tracks",
            column: "audio_file_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "album_main_artists",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "album_tracks",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "artist_gallery_images",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "genres",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "moods",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "track_featured_artists",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "track_genres",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "track_main_artists",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "track_moods",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "albums",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "artists",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "tracks",
            schema: "catalog");
    }
}
