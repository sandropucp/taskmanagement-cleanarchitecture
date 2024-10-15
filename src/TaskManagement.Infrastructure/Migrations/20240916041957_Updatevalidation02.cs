using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Updatevalidation02 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Tasks");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Tasks");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "Tasks");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Tasks");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Tasks",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAt",
            table: "Tasks",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
