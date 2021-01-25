using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class TemplateMigrationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TemplateHistory");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TemplateHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedDate",
                table: "TemplateHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifierId",
                table: "TemplateHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatorId",
                table: "Templates",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ModifierId",
                table: "Templates",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHistory_ModifierId",
                table: "TemplateHistory",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ListRecipients_ListId",
                table: "ListRecipients",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_ListRecipients_RecipientId",
                table: "ListRecipients",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListRecipients_Lists_ListId",
                table: "ListRecipients",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListRecipients_Recipients_RecipientId",
                table: "ListRecipients",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateHistory_Users_ModifierId",
                table: "TemplateHistory",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_CreatorId",
                table: "Templates",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_ModifierId",
                table: "Templates",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListRecipients_Lists_ListId",
                table: "ListRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_ListRecipients_Recipients_RecipientId",
                table: "ListRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Users_ModifierId",
                table: "TemplateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_CreatorId",
                table: "Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_ModifierId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CreatorId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_ModifierId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_ModifierId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_ListRecipients_ListId",
                table: "ListRecipients");

            migrationBuilder.DropIndex(
                name: "IX_ListRecipients_RecipientId",
                table: "ListRecipients");

            migrationBuilder.DropColumn(
                name: "EditedDate",
                table: "TemplateHistory");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "TemplateHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TemplateHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "TemplateHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
