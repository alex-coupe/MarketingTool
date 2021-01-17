using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChangesToRecipientAndAddRecipientSchemaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailJobHistory");

            migrationBuilder.DropTable(
                name: "EmailJobs");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipients");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SchemaValues",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "CampaignJobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignJobHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignJobHistory_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignJobHistory_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignJobs_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientSchemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Schema = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientSchemas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignJobHistory_CampaignId",
                table: "CampaignJobHistory",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignJobHistory_EmailStatusId",
                table: "CampaignJobHistory",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignJobs_CampaignId",
                table: "CampaignJobs",
                column: "CampaignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignJobHistory");

            migrationBuilder.DropTable(
                name: "CampaignJobs");

            migrationBuilder.DropTable(
                name: "RecipientSchemas");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Campaigns");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmailJobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    EmailStatusId = table.Column<int>(type: "int", nullable: false),
                    ProcessedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailJobHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailJobHistory_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailJobHistory_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailJobs_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailJobHistory_CampaignId",
                table: "EmailJobHistory",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJobHistory_EmailStatusId",
                table: "EmailJobHistory",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJobs_CampaignId",
                table: "EmailJobs",
                column: "CampaignId");
        }
    }
}
