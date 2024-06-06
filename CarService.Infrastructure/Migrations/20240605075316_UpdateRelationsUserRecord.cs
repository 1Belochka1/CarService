using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsUserRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_UserInfos_ClientId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_UserInfos_ClientId",
                table: "Records",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_UserInfos_ClientId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_UserInfos_ClientId",
                table: "Records",
                column: "ClientId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
