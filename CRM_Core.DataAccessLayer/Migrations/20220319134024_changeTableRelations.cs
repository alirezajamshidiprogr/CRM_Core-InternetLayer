using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class changeTableRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContact",
                table: "TelPhones");

            migrationBuilder.DropColumn(
                name: "isContact",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "TelPhones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TelPhones");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Address");

            migrationBuilder.AddColumn<bool>(
                name: "IsContact",
                table: "TelPhones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isContact",
                table: "Address",
                type: "bit",
                nullable: true);
        }
    }
}
