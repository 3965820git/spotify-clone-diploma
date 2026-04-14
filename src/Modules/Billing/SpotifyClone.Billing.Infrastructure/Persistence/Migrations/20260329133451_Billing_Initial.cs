using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Billing.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Billing_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "billing");

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "billing",
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
            name: "subscriptions",
            schema: "billing",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                external_customer_id = table.Column<string>(type: "text", nullable: false),
                external_subscription_id = table.Column<string>(type: "text", nullable: true),
                status = table.Column<int>(type: "integer", nullable: false),
                current_period_start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                current_period_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                cancel_at_period_end = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_subscriptions", x => x.id));

        migrationBuilder.CreateIndex(
            name: "IX_outbox_messages_processed_on",
            schema: "billing",
            table: "outbox_messages",
            column: "processed_on");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "billing");

        migrationBuilder.DropTable(
            name: "subscriptions",
            schema: "billing");
    }
}
