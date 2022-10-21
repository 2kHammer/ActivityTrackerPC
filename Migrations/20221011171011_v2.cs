using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityTrackerPC.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationDurations_sessions_SessionId",
                table: "applicationDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_users_UserId",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Session");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_UserId",
                table: "Session",
                newName: "IX_Session_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndingTime",
                table: "Session",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingTime",
                table: "Session",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                table: "Session",
                column: "SessionId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationDurations_Session_SessionId",
                table: "applicationDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_users_UserId",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "EndingTime",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Session");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "sessions");

            migrationBuilder.RenameIndex(
                name: "IX_Session_UserId",
                table: "sessions",
                newName: "IX_sessions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationDurations_sessions_SessionId",
                table: "applicationDurations",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_users_UserId",
                table: "sessions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
