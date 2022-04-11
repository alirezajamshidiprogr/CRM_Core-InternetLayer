using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditRelatios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isContact",
                table: "PeopleVirtual");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Personnel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PeopleVirtual",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_AddressId",
                table: "Personnel",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AddressId",
                table: "Contacts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Address_AddressId",
                table: "Contacts",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Address_AddressId",
                table: "Personnel",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Address_AddressId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Address_AddressId",
                table: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_Personnel_AddressId",
                table: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AddressId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PeopleVirtual");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Contacts");

            migrationBuilder.AddColumn<bool>(
                name: "isContact",
                table: "PeopleVirtual",
                type: "bit",
                nullable: true);
        }
    }
}
