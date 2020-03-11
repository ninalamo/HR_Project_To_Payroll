using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Persistence.Migrations
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
                    LastName = table.Column<string>(nullable: true),
                    CompanyEmail = table.Column<string>(nullable: false),
                    PersonalEmail = table.Column<string>(nullable: false),
                    ReportsTo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EmployeeID", x => x.ID);
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
                name: "Approvers",
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
                    Level = table.Column<int>(nullable: false),
                    TypeOfRequest = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Approvers_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    Location = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RawLogs_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTrackers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    RequestorID = table.Column<Guid>(nullable: false),
                    TypeOfRequest = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTrackers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestTrackers_Employees_RequestorID",
                        column: x => x.RequestorID,
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
                    table.PrimaryKey("ScheduleID", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "ApprovalTrackers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ApproverID = table.Column<long>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    RequestTrackerID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalTrackers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApprovalTrackers_Approvers_ApproverID",
                        column: x => x.ApproverID,
                        principalTable: "Approvers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalTrackers_RequestTrackers_RequestTrackerID",
                        column: x => x.RequestTrackerID,
                        principalTable: "RequestTrackers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OverTimeRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    TrackerID = table.Column<long>(nullable: false),
                    EmployeeID = table.Column<Guid>(nullable: false),
                    TimeStart = table.Column<DateTimeOffset>(nullable: false),
                    TimeEnd = table.Column<DateTimeOffset>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    ShiftDate = table.Column<DateTimeOffset>(nullable: false),
                    Purpose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OverTimeRequests_RequestTrackers_TrackerID",
                        column: x => x.TrackerID,
                        principalTable: "RequestTrackers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "CompanyEmail", "CreatedBy", "CreatedOn", "EmployeeNumber", "FirstName", "IsActive", "LastName", "ModifiedBy", "ModifiedOn", "PersonalEmail", "ReportsTo" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "sabin.alessa@outlook.com", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "112717", "Sabin Alessa", true, "Alamo", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "sabin.alessa@gmail.com", null });

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

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "EmployeeID", "IsActive", "ModifiedBy", "ModifiedOn", "ShiftID" },
                values: new object[] { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000001"), true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3L });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalTrackers_ApproverID",
                table: "ApprovalTrackers",
                column: "ApproverID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalTrackers_RequestTrackerID",
                table: "ApprovalTrackers",
                column: "RequestTrackerID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvers_EmployeeID",
                table: "Approvers",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidatedTimeSheets_EmployeeID",
                table: "ConsolidatedTimeSheets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyEmail",
                table: "Employees",
                column: "CompanyEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeNumber",
                table: "Employees",
                column: "EmployeeNumber",
                unique: true,
                filter: "[EmployeeNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName_LastName",
                table: "Employees",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeRequests_TrackerID",
                table: "OverTimeRequests",
                column: "TrackerID");

            migrationBuilder.CreateIndex(
                name: "IX_RawLogs_EmployeeID",
                table: "RawLogs",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTrackers_RequestorID",
                table: "RequestTrackers",
                column: "RequestorID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ShiftID",
                table: "Schedules",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeID_ShiftID",
                table: "Schedules",
                columns: new[] { "EmployeeID", "ShiftID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalTrackers");

            migrationBuilder.DropTable(
                name: "ConsolidatedTimeSheets");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "OverTimeRequests");

            migrationBuilder.DropTable(
                name: "RawLogs");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Approvers");

            migrationBuilder.DropTable(
                name: "RequestTrackers");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
