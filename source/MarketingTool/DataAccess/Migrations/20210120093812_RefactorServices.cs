using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RefactorServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailStatusId",
                table: "CampaignJobHistory",
                newName: "EmailStatusCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedTimestamp",
                table: "Campaigns",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedTimestamp",
                table: "Campaigns");

            migrationBuilder.RenameColumn(
                name: "EmailStatusCode",
                table: "CampaignJobHistory",
                newName: "EmailStatusId");
        }
    }
}
