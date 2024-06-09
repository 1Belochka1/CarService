using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MastersServices");

            migrationBuilder.DropTable(
                name: "RecordsMasters");

            migrationBuilder.DropTable(
                name: "RecordsServices");

            migrationBuilder.CreateTable(
                name: "RequestMasters",
                columns: table => new
                {
                    MastersId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMasters", x => new { x.MastersId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_RequestMasters_Request_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestMasters_UserAuths_MastersId",
                        column: x => x.MastersId,
                        principalTable: "UserAuths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_RequestMasters_WorksId",
                table: "RequestMasters",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestService_ServicesId",
                table: "RequestService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserAuth_ServicesId",
                table: "ServiceUserAuth",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestMasters");

            migrationBuilder.DropTable(
                name: "RequestService");

            migrationBuilder.DropTable(
                name: "ServiceUserAuth");

            migrationBuilder.CreateTable(
                name: "MastersServices",
                columns: table => new
                {
                    MastersId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MastersServices", x => new { x.MastersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_MastersServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MastersServices_UserAuths_MastersId",
                        column: x => x.MastersId,
                        principalTable: "UserAuths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsMasters",
                columns: table => new
                {
                    MastersId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsMasters", x => new { x.MastersId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_RecordsMasters_Request_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordsMasters_UserAuths_MastersId",
                        column: x => x.MastersId,
                        principalTable: "UserAuths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordsServices",
                columns: table => new
                {
                    RecordsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsServices", x => new { x.RecordsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_RecordsServices_Request_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordsServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MastersServices_ServicesId",
                table: "MastersServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsMasters_WorksId",
                table: "RecordsMasters",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsServices_ServicesId",
                table: "RecordsServices",
                column: "ServicesId");
        }
    }
}
