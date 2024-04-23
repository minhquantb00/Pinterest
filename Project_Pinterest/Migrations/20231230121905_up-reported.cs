using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Pinterest.Migrations
{
    /// <inheritdoc />
    public partial class upreported : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports");

            migrationBuilder.AlterColumn<int>(
                name: "UserReportedId",
                table: "reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports",
                column: "UserReportedId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports");

            migrationBuilder.AlterColumn<int>(
                name: "UserReportedId",
                table: "reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports",
                column: "UserReportedId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
