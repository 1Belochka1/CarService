using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    UserInfoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarInfo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VisitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsTransferred = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CompleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Status = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    TimeRecordId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_UserInfos_ClientId",
                        column: x => x.ClientId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAuths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAuths_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAuths_UserInfos_Id",
                        column: x => x.Id,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsShowLending = table.Column<bool>(type: "boolean", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "DaysRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Offset = table.Column<short>(type: "smallint", nullable: false),
                    IsWeekend = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaysRecords_Records_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    IsBusy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesRecords_DaysRecords_DayRecordId",
                        column: x => x.DayRecordId,
                        principalTable: "DaysRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesRecords_Request_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimesRecords_UserInfos_ClientId",
                        column: x => x.ClientId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Master" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysRecords_CalendarId",
                table: "DaysRecords",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserInfoId",
                table: "Images",
                column: "UserInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MastersServices_ServicesId",
                table: "MastersServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ServiceId",
                table: "Requests",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecordsMasters_WorksId",
                table: "RecordsMasters",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordsServices_ServicesId",
                table: "RecordsServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ClientId",
                table: "Request",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ImageId",
                table: "Services",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Name",
                table: "Services",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimesRecords_ClientId",
                table: "TimesRecords",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesRecords_DayRecordId",
                table: "TimesRecords",
                column: "DayRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesRecords_RecordId",
                table: "TimesRecords",
                column: "RecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuths_Phone",
                table: "UserAuths",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAuths_RoleId",
                table: "UserAuths",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Phone",
                table: "UserInfos",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MastersServices");

            migrationBuilder.DropTable(
                name: "RecordsMasters");

            migrationBuilder.DropTable(
                name: "RecordsServices");

            migrationBuilder.DropTable(
                name: "TimesRecords");

            migrationBuilder.DropTable(
                name: "UserAuths");

            migrationBuilder.DropTable(
                name: "DaysRecords");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
