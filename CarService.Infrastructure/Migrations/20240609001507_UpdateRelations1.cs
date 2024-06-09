using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestService");

            migrationBuilder.DropTable(
                name: "ServiceUserAuth");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "UserAuths",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Services",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuths_ServiceId",
                table: "UserAuths",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_RequestId",
                table: "Services",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Request_RequestId",
                table: "Services",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuths_Services_ServiceId",
                table: "UserAuths",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Request_RequestId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuths_Services_ServiceId",
                table: "UserAuths");

            migrationBuilder.DropIndex(
                name: "IX_UserAuths_ServiceId",
                table: "UserAuths");

            migrationBuilder.DropIndex(
                name: "IX_Services_RequestId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "UserAuths");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "RequestService",
                columns: table => new
                {
                    RecordsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestService", x => new { x.RecordsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_RequestService_Request_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUserAuth",
                columns: table => new
                {
                    MastersId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUserAuth", x => new { x.MastersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ServiceUserAuth_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUserAuth_UserAuths_MastersId",
                        column: x => x.MastersId,
                        principalTable: "UserAuths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestService_ServicesId",
                table: "RequestService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserAuth_ServicesId",
                table: "ServiceUserAuth",
                column: "ServicesId");
        }
    }
}
