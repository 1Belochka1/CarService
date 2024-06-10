using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuths_UserInfos_Id",
                table: "UserAuths");

            migrationBuilder.AddColumn<Guid>(
                name: "UsesInfoId",
                table: "UserAuths",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAuths_UsesInfoId",
                table: "UserAuths",
                column: "UsesInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuths_UserInfos_UsesInfoId",
                table: "UserAuths",
                column: "UsesInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuths_UserInfos_UsesInfoId",
                table: "UserAuths");

            migrationBuilder.DropIndex(
                name: "IX_UserAuths_UsesInfoId",
                table: "UserAuths");

            migrationBuilder.DropColumn(
                name: "UsesInfoId",
                table: "UserAuths");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuths_UserInfos_Id",
                table: "UserAuths",
                column: "Id",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
