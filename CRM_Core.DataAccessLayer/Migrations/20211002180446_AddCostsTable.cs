using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class AddCostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromTarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalonCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostGroupId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<int>(type: "int", nullable: false),
                    CostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    F_RegisterDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_EditDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    M_EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    F_CostDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_CostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillCostsId = table.Column<int>(type: "int", nullable: true),
                    TransferCostsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalonCosts_BillCosts_BillCostsId",
                        column: x => x.BillCostsId,
                        principalTable: "BillCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalonCosts_TransferCosts_TransferCostsId",
                        column: x => x.TransferCostsId,
                        principalTable: "TransferCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalonCosts");

            migrationBuilder.DropTable(
                name: "BillCosts");

            migrationBuilder.DropTable(
                name: "TransferCosts");
        }
    }
}
