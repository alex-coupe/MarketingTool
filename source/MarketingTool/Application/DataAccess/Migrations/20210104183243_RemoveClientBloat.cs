using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveClientBloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_SubscriptionLevels_SubscriptionLevelId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SubscriptionLevelId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressOne",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressTwo",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TelephoneOne",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TelephoneTwo",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressOne",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressTwo",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Clients",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Clients",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Clients",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneOne",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneTwo",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SubscriptionLevelId",
                table: "Clients",
                column: "SubscriptionLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_SubscriptionLevels_SubscriptionLevelId",
                table: "Clients",
                column: "SubscriptionLevelId",
                principalTable: "SubscriptionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
