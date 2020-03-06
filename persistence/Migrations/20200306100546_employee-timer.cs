using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Persistence.Migrations
{
    public partial class employeetimer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RawLogs_EmployeeID",
                table: "RawLogs",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RawLogs_Employees_EmployeeID",
                table: "RawLogs",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawLogs_Employees_EmployeeID",
                table: "RawLogs");

            migrationBuilder.DropIndex(
                name: "IX_RawLogs_EmployeeID",
                table: "RawLogs");
        }
    }
}
