using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditSalonCostsTableRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalonCosts_BillCosts_BillCostsId",
                table: "SalonCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonCosts_TransferCosts_TransferCostsId",
                table: "SalonCosts");

            migrationBuilder.DropIndex(
                name: "IX_SalonCosts_BillCostsId",
                table: "SalonCosts");

            migrationBuilder.DropIndex(
                name: "IX_SalonCosts_TransferCostsId",
                table: "SalonCosts");

            migrationBuilder.DropColumn(
                name: "BillCostsId",
                table: "SalonCosts");

            migrationBuilder.DropColumn(
                name: "CostGroupId",
                table: "SalonCosts");

            migrationBuilder.RenameColumn(
                name: "TransferCostsId",
                table: "SalonCosts",
                newName: "RelatinveId");

            migrationBuilder.RenameColumn(
                name: "CostType",
                table: "SalonCosts",
                newName: "CostTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelatinveId",
                table: "SalonCosts",
                newName: "TransferCostsId");

            migrationBuilder.RenameColumn(
                name: "CostTypeId",
                table: "SalonCosts",
                newName: "CostType");

            migrationBuilder.AddColumn<int>(
                name: "BillCostsId",
                table: "SalonCosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CostGroupId",
                table: "SalonCosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalonCosts_BillCostsId",
                table: "SalonCosts",
                column: "BillCostsId",
                unique: true,
                filter: "[BillCostsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SalonCosts_TransferCostsId",
                table: "SalonCosts",
                column: "TransferCostsId",
                unique: true,
                filter: "[TransferCostsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SalonCosts_BillCosts_BillCostsId",
                table: "SalonCosts",
                column: "BillCostsId",
                principalTable: "BillCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonCosts_TransferCosts_TransferCostsId",
                table: "SalonCosts",
                column: "TransferCostsId",
                principalTable: "TransferCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
