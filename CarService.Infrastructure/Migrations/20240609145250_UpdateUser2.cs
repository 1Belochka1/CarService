using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserInfos_ClientId",
                table: "Request");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserInfos_ClientId",
                table: "Request",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_UserInfos_ClientId",
                table: "Request");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_UserInfos_ClientId",
                table: "Request",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
