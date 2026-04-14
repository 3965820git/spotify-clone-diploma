using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Streaming_Add_OutboxMessages_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "streaming",
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

        migrationBuilder.CreateIndex(
            name: "IX_outbox_messages_processed_on",
            schema: "streaming",
            table: "outbox_messages",
            column: "processed_on");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        => migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "streaming");
}
