using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditSalonCostsTableColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillName",
                table: "BillCosts");

            migrationBuilder.AlterColumn<int>(
                name: "TransferType",
                table: "TransferCosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillType",
                table: "BillCosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillType",
                table: "BillCosts");

            migrationBuilder.AlterColumn<string>(
                name: "TransferType",
                table: "TransferCosts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BillName",
                table: "BillCosts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
