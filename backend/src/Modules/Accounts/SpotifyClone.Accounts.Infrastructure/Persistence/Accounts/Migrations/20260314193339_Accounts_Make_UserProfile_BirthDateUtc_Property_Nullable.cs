using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Migrations;

/// <inheritdoc />
public partial class Accounts_Make_UserProfile_BirthDateUtc_Property_Nullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "birth_date",
            schema: "accounts",
            table: "user_profiles");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "birth_date_utc",
            schema: "accounts",
            table: "user_profiles",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "birth_date_utc",
            schema: "accounts",
            table: "user_profiles");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "birth_date",
            schema: "accounts",
            table: "user_profiles",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
    }
}
