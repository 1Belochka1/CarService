using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServicUpdateImage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CalendarId",
                table: "Services");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarRecords_ServiceId",
                table: "CalendarRecords",
                column: "ServiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarRecords_Services_ServiceId",
                table: "CalendarRecords",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarRecords_Services_ServiceId",
                table: "CalendarRecords");

            migrationBuilder.DropIndex(
                name: "IX_CalendarRecords_ServiceId",
                table: "CalendarRecords");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CalendarId",
                table: "Services",
                column: "CalendarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_CalendarRecords_CalendarId",
                table: "Services",
                column: "CalendarId",
                principalTable: "CalendarRecords",
                principalColumn: "Id");
        }
    }
}
