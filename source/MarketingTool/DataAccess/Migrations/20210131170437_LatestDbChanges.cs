using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class LatestDbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimestepId",
                table: "Campaigns");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ModifierId",
                table: "Lists",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Users_CreatorId",
                table: "Campaigns",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Users_ModifierId",
                table: "Campaigns",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_CreatorId",
                table: "Lists",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Users_ModifierId",
                table: "Lists",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_ModifierId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_CreatorId",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_ModifierId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ModifierId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns");

            migrationBuilder.AddColumn<int>(
                name: "TimestepId",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
