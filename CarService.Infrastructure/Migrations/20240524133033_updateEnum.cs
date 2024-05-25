using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:record_priority", "low,normal,high,very_high")
                .Annotation("Npgsql:Enum:record_status", "new,processing,awaiting,work,done")
                .OldAnnotation("Npgsql:Enum:record_priority", "low,normal,high,very_high")
                .OldAnnotation("Npgsql:Enum:record_status", "new,work,done");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 24, 13, 30, 32, 692, DateTimeKind.Utc).AddTicks(341),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 24, 13, 28, 16, 327, DateTimeKind.Utc).AddTicks(2321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 24, 13, 30, 32, 690, DateTimeKind.Utc).AddTicks(8054),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 24, 13, 28, 16, 326, DateTimeKind.Utc).AddTicks(492));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:record_priority", "low,normal,high,very_high")
                .Annotation("Npgsql:Enum:record_status", "new,work,done")
                .OldAnnotation("Npgsql:Enum:record_priority", "low,normal,high,very_high")
                .OldAnnotation("Npgsql:Enum:record_status", "new,processing,awaiting,work,done");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 24, 13, 28, 16, 327, DateTimeKind.Utc).AddTicks(2321),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 24, 13, 30, 32, 692, DateTimeKind.Utc).AddTicks(341));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Chats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 24, 13, 28, 16, 326, DateTimeKind.Utc).AddTicks(492),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 24, 13, 30, 32, 690, DateTimeKind.Utc).AddTicks(8054));
        }
    }
}
