using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class LotsOfChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignJobHistory_Campaigns_CampaignId",
                table: "CampaignJobHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignJobHistory_EmailStatuses_EmailStatusId",
                table: "CampaignJobHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Lists_ListId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Templates_TemplateId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Timesteps_TimestepId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_ModifierId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_SubscriptionLevels_SubscriptionLevelId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Clients_ClientId",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_CreatorId",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Users_ModifierId",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_Clients_ClientId",
                table: "Recipients");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Users_CreatorId",
                table: "TemplateHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Clients_ClientId",
                table: "Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_CreatorId",
                table: "Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_ModifierId",
                table: "Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateSynonyms_Clients_ClientId",
                table: "TemplateSynonyms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvites_Users_InvitingUserId",
                table: "UserInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users");

          
            migrationBuilder.DropIndex(
                name: "IX_Users_ClientId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserInvites_InvitingUserId",
                table: "UserInvites");

            migrationBuilder.DropIndex(
                name: "IX_TemplateSynonyms_ClientId",
                table: "TemplateSynonyms");

            migrationBuilder.DropIndex(
                name: "IX_Templates_ClientId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CreatorId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_ModifierId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_CreatorId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_ClientId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ClientId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ModifierId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SubscriptionLevelId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ClientId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ListId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_TemplateId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_TimestepId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_CampaignJobHistory_CampaignId",
                table: "CampaignJobHistory");

            migrationBuilder.DropIndex(
                name: "IX_CampaignJobHistory_EmailStatusId",
                table: "CampaignJobHistory");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "LastSent",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "HourToProcess",
                table: "CampaignJobs");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Campaigns",
                newName: "SendDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingDateTime",
                table: "CampaignJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListRecipients");

            migrationBuilder.DropColumn(
                name: "ProcessingDateTime",
                table: "CampaignJobs");

            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Campaigns",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Campaigns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSent",
                table: "Campaigns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HourToProcess",
                table: "CampaignJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ListRecipient",
                columns: table => new
                {
                    ListsId = table.Column<int>(type: "int", nullable: false),
                    RecipientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListRecipient", x => new { x.ListsId, x.RecipientsId });
                    table.ForeignKey(
                        name: "FK_ListRecipient_Lists_ListsId",
                        column: x => x.ListsId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListRecipient_Recipients_RecipientsId",
                        column: x => x.RecipientsId,
                        principalTable: "Recipients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientId",
                table: "Users",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvites_InvitingUserId",
                table: "UserInvites",
                column: "InvitingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSynonyms_ClientId",
                table: "TemplateSynonyms",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ClientId",
                table: "Templates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatorId",
                table: "Templates",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_ModifierId",
                table: "Templates",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHistory_CreatorId",
                table: "TemplateHistory",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_ClientId",
                table: "Recipients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ClientId",
                table: "Lists",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ModifierId",
                table: "Lists",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SubscriptionLevelId",
                table: "Clients",
                column: "SubscriptionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ClientId",
                table: "Campaigns",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ListId",
                table: "Campaigns",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TemplateId",
                table: "Campaigns",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TimestepId",
                table: "Campaigns",
                column: "TimestepId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignJobHistory_CampaignId",
                table: "CampaignJobHistory",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignJobHistory_EmailStatusId",
                table: "CampaignJobHistory",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ListRecipient_RecipientsId",
                table: "ListRecipient",
                column: "RecipientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignJobHistory_Campaigns_CampaignId",
                table: "CampaignJobHistory",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignJobHistory_EmailStatuses_EmailStatusId",
                table: "CampaignJobHistory",
                column: "EmailStatusId",
                principalTable: "EmailStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Lists_ListId",
                table: "Campaigns",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Templates_TemplateId",
                table: "Campaigns",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Timesteps_TimestepId",
                table: "Campaigns",
                column: "TimestepId",
                principalTable: "Timesteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Clients_SubscriptionLevels_SubscriptionLevelId",
                table: "Clients",
                column: "SubscriptionLevelId",
                principalTable: "SubscriptionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Clients_ClientId",
                table: "Lists",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_Clients_ClientId",
                table: "Recipients",
                column: "ClientId",
                principalTable: "Clients",
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
                name: "FK_TemplateHistory_Users_CreatorId",
                table: "TemplateHistory",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Clients_ClientId",
                table: "Templates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateSynonyms_Clients_ClientId",
                table: "TemplateSynonyms",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvites_Users_InvitingUserId",
                table: "UserInvites",
                column: "InvitingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
