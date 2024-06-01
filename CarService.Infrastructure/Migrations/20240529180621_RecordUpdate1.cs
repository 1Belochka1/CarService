using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecordUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_DaysRecords_DayRecordId",
                table: "TimeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_Records_RecordId",
                table: "TimeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_UserInfos_ClientId",
                table: "TimeRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRecords",
                table: "TimeRecords");

            migrationBuilder.RenameTable(
                name: "TimeRecords",
                newName: "TimesRecords");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRecords_RecordId",
                table: "TimesRecords",
                newName: "IX_TimesRecords_RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRecords_DayRecordId",
                table: "TimesRecords",
                newName: "IX_TimesRecords_DayRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRecords_ClientId",
                table: "TimesRecords",
                newName: "IX_TimesRecords_ClientId");

            migrationBuilder.AddColumn<bool>(
                name: "IsWeekend",
                table: "DaysRecords",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesRecords",
                table: "TimesRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesRecords_DaysRecords_DayRecordId",
                table: "TimesRecords",
                column: "DayRecordId",
                principalTable: "DaysRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesRecords_Records_RecordId",
                table: "TimesRecords",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesRecords_UserInfos_ClientId",
                table: "TimesRecords",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesRecords_DaysRecords_DayRecordId",
                table: "TimesRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimesRecords_Records_RecordId",
                table: "TimesRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimesRecords_UserInfos_ClientId",
                table: "TimesRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesRecords",
                table: "TimesRecords");

            migrationBuilder.DropColumn(
                name: "IsWeekend",
                table: "DaysRecords");

            migrationBuilder.RenameTable(
                name: "TimesRecords",
                newName: "TimeRecords");

            migrationBuilder.RenameIndex(
                name: "IX_TimesRecords_RecordId",
                table: "TimeRecords",
                newName: "IX_TimeRecords_RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_TimesRecords_DayRecordId",
                table: "TimeRecords",
                newName: "IX_TimeRecords_DayRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_TimesRecords_ClientId",
                table: "TimeRecords",
                newName: "IX_TimeRecords_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRecords",
                table: "TimeRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_DaysRecords_DayRecordId",
                table: "TimeRecords",
                column: "DayRecordId",
                principalTable: "DaysRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_Records_RecordId",
                table: "TimeRecords",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_UserInfos_ClientId",
                table: "TimeRecords",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
