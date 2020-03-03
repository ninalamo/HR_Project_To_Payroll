using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Persistence.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locaton",
                table: "RawLogs");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "RawLogs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "CompanyEmail", "CreatedBy", "CreatedOn", "EmployeeNumber", "FirstName", "IsActive", "LastName", "ModifiedBy", "ModifiedOn", "PersonalEmail" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "sabin.alessa@outlook.com", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "112717", "Sabin Alessa", true, "Alamo", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "sabin.alessa@gmail.com" });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ID", "CreatedBy", "CreatedOn", "EmployeeID", "IsActive", "ModifiedBy", "ModifiedOn", "ShiftID" },
                values: new object[] { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000001"), true, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DropColumn(
                name: "Location",
                table: "RawLogs");

            migrationBuilder.AddColumn<string>(
                name: "Locaton",
                table: "RawLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
