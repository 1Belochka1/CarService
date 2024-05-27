using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalendarId",
                table: "Services",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 15, 54, 51, 717, DateTimeKind.Utc).AddTicks(5360),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 26, 13, 13, 40, 311, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 15, 54, 51, 716, DateTimeKind.Utc).AddTicks(217),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 26, 13, 13, 40, 310, DateTimeKind.Utc).AddTicks(6065));

            migrationBuilder.AddForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services",
                column: "CalendarId",
                principalTable: "CalendarRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalendarId",
                table: "Services",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 13, 13, 40, 311, DateTimeKind.Utc).AddTicks(8764),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 26, 15, 54, 51, 717, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 26, 13, 13, 40, 310, DateTimeKind.Utc).AddTicks(6065),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 26, 15, 54, 51, 716, DateTimeKind.Utc).AddTicks(217));

            migrationBuilder.AddForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services",
                column: "CalendarId",
                principalTable: "CalendarRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
