using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class PeopleServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.CreateTable(
                name: "PeopleServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ClerkServicesId = table.Column<int>(type: "int", nullable: false),
                    isSalonCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleServices_ClerkServices_ClerkServicesId",
                        column: x => x.ClerkServicesId,
                        principalTable: "ClerkServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleServices_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleServices_ClerkServicesId",
                table: "PeopleServices",
                column: "ClerkServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleServices_ReservationId",
                table: "PeopleServices",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeopleServices");

            migrationBuilder.CreateTable(
                name: "CustomersService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClerkServicesId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    isSalonCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersService_ClerkServices_ClerkServicesId",
                        column: x => x.ClerkServicesId,
                        principalTable: "ClerkServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersService_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersService_ClerkServicesId",
                table: "CustomersService",
                column: "ClerkServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersService_ReservationId",
                table: "CustomersService",
                column: "ReservationId");
        }
    }
}
