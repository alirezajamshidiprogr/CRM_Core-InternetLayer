using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class AddTablesPersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PersonnelLastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    AgreementTypeId = table.Column<int>(type: "int", nullable: false),
                    CertificateCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    HomeTel = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    P_Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_Birthday = table.Column<DateTime>(type: "Date", maxLength: 10, nullable: true),
                    M_InsertDate = table.Column<DateTime>(type: "datetime2", maxLength: 10, nullable: false),
                    M_EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnel_AgreementType_AgreementTypeId",
                        column: x => x.AgreementTypeId,
                        principalTable: "AgreementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelSkils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    TBASPersonenelSkilsId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelSkils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelSkils_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonnelSkils_TBASPersonenelSkils_TBASPersonenelSkilsId",
                        column: x => x.TBASPersonenelSkilsId,
                        principalTable: "TBASPersonenelSkils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelVation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    P_VacationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    M_VacationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VacationType = table.Column<int>(type: "int", nullable: false),
                    M_InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    M_EditDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelVation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelVation_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelWorkTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    P_WorkTimeDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    M_WorkTimeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    M_InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    M_EditDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelWorkTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelWorkTime_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_AgreementTypeId",
                table: "Personnel",
                column: "AgreementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelSkils_PersonnelId",
                table: "PersonnelSkils",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelSkils_TBASPersonenelSkilsId",
                table: "PersonnelSkils",
                column: "TBASPersonenelSkilsId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelVation_PersonnelId",
                table: "PersonnelVation",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelWorkTime_PersonnelId",
                table: "PersonnelWorkTime",
                column: "PersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelSkils");

            migrationBuilder.DropTable(
                name: "PersonnelVation");

            migrationBuilder.DropTable(
                name: "PersonnelWorkTime");

            migrationBuilder.DropTable(
                name: "TBASPersonenelSkils");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "AgreementType");
        }
    }
}
