using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityTrackerPC.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationDurations_application_ApplicationId",
                table: "applicationDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationDurations_Session_SessionId",
                table: "applicationDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_users_UserId",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_application",
                table: "application");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_applicationDurations",
                table: "applicationDurations");

            migrationBuilder.RenameTable(
                name: "application",
                newName: "Application");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "applicationDurations",
                newName: "ApplicationDurcation");

            migrationBuilder.RenameIndex(
                name: "IX_applicationDurations_SessionId",
                table: "ApplicationDurcation",
                newName: "IX_ApplicationDurcation_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_applicationDurations_ApplicationId",
                table: "ApplicationDurcation",
                newName: "IX_ApplicationDurcation_ApplicationId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Session_User_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDurcation_Application_ApplicationId",
                table: "ApplicationDurcation");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDurcation_Session_SessionId",
                table: "ApplicationDurcation");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_User_UserId",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationDurcation",
                table: "ApplicationDurcation");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "application");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "ApplicationDurcation",
                newName: "applicationDurations");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDurcation_SessionId",
                table: "applicationDurations",
                newName: "IX_applicationDurations_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDurcation_ApplicationId",
                table: "applicationDurations",
                newName: "IX_applicationDurations_ApplicationId");

            migrationBuilder.AlterColumn<int>(
                name: "UserName",
                table: "users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_application",
                table: "application",
                column: "ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_applicationDurations",
                table: "applicationDurations",
                column: "ApplicationDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationDurations_application_ApplicationId",
                table: "applicationDurations",
                column: "ApplicationId",
                principalTable: "application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationDurations_Session_SessionId",
                table: "applicationDurations",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_users_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
