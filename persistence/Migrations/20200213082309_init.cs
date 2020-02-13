using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EmployeeNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    BeforeCommit = table.Column<string>(nullable: true),
                    AfterCommit = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Table = table.Column<string>(nullable: true),
                    ObjectID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AuditID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RawLogs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<Guid>(nullable: false),
                    LogType = table.Column<int>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Long = table.Column<double>(nullable: false),
                    Locaton = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Start = table.Column<int>(nullable: false),
                    End = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConsolidatedTimeSheets",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TimeIn = table.Column<DateTimeOffset>(nullable: false),
                    TimeOut = table.Column<DateTimeOffset>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    OverridenBy = table.Column<string>(nullable: true),
                    OverrideDate = table.Column<DateTimeOffset>(nullable: false),
                    ReasonForOverride = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidatedTimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConsolidatedTimeSheets_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<Guid>(nullable: false),
                    ShiftID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Shifts_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "Shifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "End", "IsActive", "ModifiedBy", "ModifiedOn", "Start" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1600, true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 700 },
                    { 2L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1700, true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 800 },
                    { 3L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1730, true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 830 },
                    { 4L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1800, true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidatedTimeSheets_EmployeeID",
                table: "ConsolidatedTimeSheets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeID",
                table: "Schedules",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ShiftID",
                table: "Schedules",
                column: "ShiftID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsolidatedTimeSheets");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "RawLogs");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
