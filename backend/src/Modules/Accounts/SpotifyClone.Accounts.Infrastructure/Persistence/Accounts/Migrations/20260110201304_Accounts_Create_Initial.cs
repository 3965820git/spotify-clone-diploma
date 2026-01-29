using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Migrations;

/// <inheritdoc />
public partial class Accounts_Create_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "accounts");

        migrationBuilder.CreateTable(
            name: "refresh_tokens",
            schema: "accounts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                token_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                expires_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                revoked_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                replaced_by_token_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_refresh_tokens", x => x.id));

        migrationBuilder.CreateTable(
            name: "user_profiles",
            schema: "accounts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                display_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                birth_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                avatar_width = table.Column<int>(type: "integer", maxLength: 1024, nullable: true),
                avatar_height = table.Column<int>(type: "integer", maxLength: 1024, nullable: true),
                avatar_file_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                avatar_image_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_user_profiles", x => x.id));

        migrationBuilder.CreateIndex(
            name: "IX_refresh_tokens_token_hash",
            schema: "accounts",
            table: "refresh_tokens",
            column: "token_hash",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_refresh_tokens_user_id",
            schema: "accounts",
            table: "refresh_tokens",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "refresh_tokens",
            schema: "accounts");

        migrationBuilder.DropTable(
            name: "user_profiles",
            schema: "accounts");
    }
}
