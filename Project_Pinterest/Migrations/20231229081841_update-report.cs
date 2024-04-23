using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Pinterest.Migrations
{
    /// <inheritdoc />
    public partial class updatereport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reports_posts_PostId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserReportId",
                table: "reports");

            migrationBuilder.AlterColumn<int>(
                name: "UserReportId",
                table: "reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReportType",
                table: "reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserReportedId",
                table: "reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_reports_UserReportedId",
                table: "reports",
                column: "UserReportedId");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_posts_PostId",
                table: "reports",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserReportId",
                table: "reports",
                column: "UserReportId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports",
                column: "UserReportedId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reports_posts_PostId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserReportId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserReportedId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_reports_UserReportedId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "UserReportedId",
                table: "reports");

            migrationBuilder.AlterColumn<int>(
                name: "UserReportId",
                table: "reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_posts_PostId",
                table: "reports",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserReportId",
                table: "reports",
                column: "UserReportId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
