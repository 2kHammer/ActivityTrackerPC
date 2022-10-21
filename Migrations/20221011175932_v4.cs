using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityTrackerPC.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDurcation_Application_ApplicationId",
                table: "ApplicationDurcation");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDurcation_Session_SessionId",
                table: "ApplicationDurcation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationDurcation",
                table: "ApplicationDurcation");

            migrationBuilder.RenameTable(
                name: "ApplicationDurcation",
                newName: "ApplicationDuration");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDurcation_SessionId",
                table: "ApplicationDuration",
                newName: "IX_ApplicationDuration_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDurcation_ApplicationId",
                table: "ApplicationDuration",
                newName: "IX_ApplicationDuration_ApplicationId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationName",
                table: "Application",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationDuration",
                table: "ApplicationDuration",
                column: "ApplicationDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDuration_Application_ApplicationId",
                table: "ApplicationDuration",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDuration_Session_SessionId",
                table: "ApplicationDuration",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDuration_Application_ApplicationId",
                table: "ApplicationDuration");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDuration_Session_SessionId",
                table: "ApplicationDuration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationDuration",
                table: "ApplicationDuration");

            migrationBuilder.RenameTable(
                name: "ApplicationDuration",
                newName: "ApplicationDurcation");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDuration_SessionId",
                table: "ApplicationDurcation",
                newName: "IX_ApplicationDurcation_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDuration_ApplicationId",
                table: "ApplicationDurcation",
                newName: "IX_ApplicationDurcation_ApplicationId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationName",
                table: "Application",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationDurcation",
                table: "ApplicationDurcation",
                column: "ApplicationDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDurcation_Application_ApplicationId",
                table: "ApplicationDurcation",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDurcation_Session_SessionId",
                table: "ApplicationDurcation",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
