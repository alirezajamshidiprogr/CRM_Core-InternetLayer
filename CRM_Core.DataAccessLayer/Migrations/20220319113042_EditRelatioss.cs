using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditRelatioss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Address_AddressId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Address_AddressId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleVirtual_PeopleVirtualId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Address_AddressId",
                table: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_Personnel_AddressId",
                table: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_People_AddressId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PeopleVirtualId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AddressId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PeopleVirtualId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Personnel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeopleVirtualId",
                table: "People",
                type: "int",
                nullable: true);

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
                name: "IX_People_AddressId",
                table: "People",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PeopleVirtualId",
                table: "People",
                column: "PeopleVirtualId");

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
                name: "FK_People_Address_AddressId",
                table: "People",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleVirtual_PeopleVirtualId",
                table: "People",
                column: "PeopleVirtualId",
                principalTable: "PeopleVirtual",
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
    }
}
