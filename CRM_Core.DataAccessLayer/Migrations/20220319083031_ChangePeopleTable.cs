using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class ChangePeopleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "People");

            migrationBuilder.DropColumn(
                name: "HomeTel",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Mobile1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Mobile2",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Mobile3",
                table: "People");

            migrationBuilder.DropColumn(
                name: "WorkTel",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "P_Birthday",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressId",
                table: "People",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PeopleVirtualId",
                table: "People",
                column: "PeopleVirtualId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Address_AddressId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleVirtual_PeopleVirtualId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AddressId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_PeopleVirtualId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PeopleVirtualId",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "P_Birthday",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTel",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile1",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile2",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile3",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkTel",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }
    }
}
