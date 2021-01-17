using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddSentFlagsToResetsAndInvites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TimeSteps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "TemplateSynonyms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ResetSent",
                table: "PasswordResets",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "IX_PasswordResets_UserId",
                table: "PasswordResets",
                column: "UserId");

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
                name: "IX_EmailJobs_CampaignId",
                table: "EmailJobs",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJobHistory_CampaignId",
                table: "EmailJobHistory",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJobHistory_EmailStatusId",
                table: "EmailJobHistory",
                column: "EmailStatusId");

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
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TemplateId",
                table: "Campaigns",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TimeStepId",
                table: "Campaigns",
                column: "TimeStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns",
                column: "ClientId",
                principalTable: "Clients",
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
                name: "FK_Campaigns_TimeSteps_TimeStepId",
                table: "Campaigns",
                column: "TimeStepId",
                principalTable: "TimeSteps",
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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_SubscriptionLevels_SubscriptionLevelId",
                table: "Clients",
                column: "SubscriptionLevelId",
                principalTable: "SubscriptionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailJobHistory_Campaigns_CampaignId",
                table: "EmailJobHistory",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailJobHistory_EmailStatuses_EmailStatusId",
                table: "EmailJobHistory",
                column: "EmailStatusId",
                principalTable: "EmailStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailJobs_Campaigns_CampaignId",
                table: "EmailJobs",
                column: "CampaignId",
                principalTable: "Campaigns",
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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResets_Users_UserId",
                table: "PasswordResets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_Clients_ClientId",
                table: "Recipients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_CreatorId",
                table: "Templates",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_ModifierId",
                table: "Templates",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Templates_TemplateId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_TimeSteps_TimeStepId",
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
                name: "FK_EmailJobHistory_Campaigns_CampaignId",
                table: "EmailJobHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailJobHistory_EmailStatuses_EmailStatusId",
                table: "EmailJobHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailJobs_Campaigns_CampaignId",
                table: "EmailJobs");

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
                name: "FK_PasswordResets_Users_UserId",
                table: "PasswordResets");

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
                name: "IX_PasswordResets_UserId",
                table: "PasswordResets");

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
                name: "IX_EmailJobs_CampaignId",
                table: "EmailJobs");

            migrationBuilder.DropIndex(
                name: "IX_EmailJobHistory_CampaignId",
                table: "EmailJobHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmailJobHistory_EmailStatusId",
                table: "EmailJobHistory");

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
                name: "IX_Campaigns_ModifierId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_TemplateId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_TimeStepId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "TemplateSynonyms");

            migrationBuilder.DropColumn(
                name: "ResetSent",
                table: "PasswordResets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TimeSteps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
