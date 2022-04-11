using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditTableTelPhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_People_PeopleId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleVirtual_People_PeopleId",
                table: "PeopleVirtual");

            migrationBuilder.DropIndex(
                name: "IX_PeopleVirtual_PeopleId",
                table: "PeopleVirtual");

            migrationBuilder.DropIndex(
                name: "IX_Address_PeopleId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PeopleVirtual",
                newName: "RelativeId");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "Address",
                newName: "RelativeId");

            migrationBuilder.CreateTable(
                name: "TelPhones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RelativeId = table.Column<int>(type: "int", nullable: false),
                    IsContact = table.Column<bool>(type: "bit", nullable: false),
                    TBASPhoneTypesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelPhones_TBASPhoneTypes_TBASPhoneTypesId",
                        column: x => x.TBASPhoneTypesId,
                        principalTable: "TBASPhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelPhones_TBASPhoneTypesId",
                table: "TelPhones",
                column: "TBASPhoneTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelPhones");

            migrationBuilder.RenameColumn(
                name: "RelativeId",
                table: "PeopleVirtual",
                newName: "PeopleId");

            migrationBuilder.RenameColumn(
                name: "RelativeId",
                table: "Address",
                newName: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleVirtual_PeopleId",
                table: "PeopleVirtual",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PeopleId",
                table: "Address",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_People_PeopleId",
                table: "Address",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleVirtual_People_PeopleId",
                table: "PeopleVirtual",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
