using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddAudit01 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UpdatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Tasks",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "CreatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "CategoryId",
            table: "Tasks",
            type: "uniqueidentifier",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier");

        migrationBuilder.CreateTable(
            name: "AuditEntries",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StartTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                Succeded = table.Column<bool>(type: "bit", nullable: false),
                ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_AuditEntries", x => x.Id));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditEntries");

        migrationBuilder.AlterColumn<string>(
            name: "UpdatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Tasks",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<string>(
            name: "CreatedBy",
            table: "Tasks",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<Guid>(
            name: "CategoryId",
            table: "Tasks",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier",
            oldNullable: true);
    }
}
