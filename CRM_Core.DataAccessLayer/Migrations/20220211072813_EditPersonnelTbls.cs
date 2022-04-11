using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditPersonnelTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_AgreementType_AgreementTypeId",
                table: "Personnel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelSkils_TBASPersonenelSkils_TBASPersonenelSkilsId",
                table: "PersonnelSkils");

            migrationBuilder.DropTable(
                name: "AgreementType");

            migrationBuilder.DropTable(
                name: "TBASPersonenelSkils");

            migrationBuilder.RenameColumn(
                name: "TBASPersonenelSkilsId",
                table: "PersonnelSkils",
                newName: "TBASServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelSkils_TBASPersonenelSkilsId",
                table: "PersonnelSkils",
                newName: "IX_PersonnelSkils_TBASServicesId");

            migrationBuilder.RenameColumn(
                name: "AgreementTypeId",
                table: "Personnel",
                newName: "TBASAgreementTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnel_AgreementTypeId",
                table: "Personnel",
                newName: "IX_Personnel_TBASAgreementTypeId");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceNumber",
                table: "Personnel",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonnelFatherName",
                table: "Personnel",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBASAgreementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASAgreementType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_TBASAgreementType_TBASAgreementTypeId",
                table: "Personnel",
                column: "TBASAgreementTypeId",
                principalTable: "TBASAgreementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelSkils_TBASServices_TBASServicesId",
                table: "PersonnelSkils",
                column: "TBASServicesId",
                principalTable: "TBASServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_TBASAgreementType_TBASAgreementTypeId",
                table: "Personnel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelSkils_TBASServices_TBASServicesId",
                table: "PersonnelSkils");

            migrationBuilder.DropTable(
                name: "TBASAgreementType");

            migrationBuilder.DropColumn(
                name: "InsuranceNumber",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "PersonnelFatherName",
                table: "Personnel");

            migrationBuilder.RenameColumn(
                name: "TBASServicesId",
                table: "PersonnelSkils",
                newName: "TBASPersonenelSkilsId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelSkils_TBASServicesId",
                table: "PersonnelSkils",
                newName: "IX_PersonnelSkils_TBASPersonenelSkilsId");

            migrationBuilder.RenameColumn(
                name: "TBASAgreementTypeId",
                table: "Personnel",
                newName: "AgreementTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnel_TBASAgreementTypeId",
                table: "Personnel",
                newName: "IX_Personnel_AgreementTypeId");

            migrationBuilder.CreateTable(
                name: "AgreementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBASPersonenelSkils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASPersonenelSkils", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_AgreementType_AgreementTypeId",
                table: "Personnel",
                column: "AgreementTypeId",
                principalTable: "AgreementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelSkils_TBASPersonenelSkils_TBASPersonenelSkilsId",
                table: "PersonnelSkils",
                column: "TBASPersonenelSkilsId",
                principalTable: "TBASPersonenelSkils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
