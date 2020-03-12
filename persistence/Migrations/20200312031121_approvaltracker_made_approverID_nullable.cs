using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Persistence.Migrations
{
    public partial class approvaltracker_made_approverID_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalTrackers_Approvers_ApproverID",
                table: "ApprovalTrackers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Approvers",
                table: "Approvers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "RequestTrackers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverID",
                table: "ApprovalTrackers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "ApproverEmail",
                table: "ApprovalTrackers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "ApproverID",
                table: "Approvers",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvers_ID_TypeOfRequest_Level",
                table: "Approvers",
                columns: new[] { "ID", "TypeOfRequest", "Level" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalTrackers_Approvers_ApproverID",
                table: "ApprovalTrackers",
                column: "ApproverID",
                principalTable: "Approvers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalTrackers_Approvers_ApproverID",
                table: "ApprovalTrackers");

            migrationBuilder.DropPrimaryKey(
                name: "ApproverID",
                table: "Approvers");

            migrationBuilder.DropIndex(
                name: "IX_Approvers_ID_TypeOfRequest_Level",
                table: "Approvers");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "RequestTrackers");

            migrationBuilder.DropColumn(
                name: "ApproverEmail",
                table: "ApprovalTrackers");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverID",
                table: "ApprovalTrackers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Approvers",
                table: "Approvers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalTrackers_Approvers_ApproverID",
                table: "ApprovalTrackers",
                column: "ApproverID",
                principalTable: "Approvers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
