using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Updatevalidation01 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Comments");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Comments");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "Comments");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Comments");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Attachments");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Attachments");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "Attachments");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Attachments");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Users",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAt",
            table: "Users",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Users",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Comments",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Comments",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAt",
            table: "Comments",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Comments",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Attachments",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Attachments",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAt",
            table: "Attachments",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Attachments",
            type: "nvarchar(max)",
            nullable: true);
    }
}
