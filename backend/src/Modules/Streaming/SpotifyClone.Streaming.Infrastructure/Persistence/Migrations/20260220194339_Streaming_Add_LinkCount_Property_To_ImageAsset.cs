using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Add_LinkCount_Property_To_ImageAsset : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
        => migrationBuilder.AddColumn<int>(
            name: "link_count",
            schema: "streaming",
            table: "image_assets",
            type: "integer",
            nullable: false,
            defaultValue: 0);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.DropColumn(
            name: "link_count",
            schema: "streaming",
            table: "image_assets");
}
