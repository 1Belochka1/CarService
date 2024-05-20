using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_record : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Records",
                newName: "CreateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteTime",
                table: "Records",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 18, 23, 5, 46, 124, DateTimeKind.Utc).AddTicks(4938),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 13, 17, 15, 3, 783, DateTimeKind.Utc).AddTicks(3471));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 18, 23, 5, 46, 123, DateTimeKind.Utc).AddTicks(1830),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 13, 17, 15, 3, 782, DateTimeKind.Utc).AddTicks(1030));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteTime",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Records",
                newName: "Time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 17, 15, 3, 783, DateTimeKind.Utc).AddTicks(3471),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 18, 23, 5, 46, 124, DateTimeKind.Utc).AddTicks(4938));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 17, 15, 3, 782, DateTimeKind.Utc).AddTicks(1030),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 18, 23, 5, 46, 123, DateTimeKind.Utc).AddTicks(1830));
        }
    }
}
