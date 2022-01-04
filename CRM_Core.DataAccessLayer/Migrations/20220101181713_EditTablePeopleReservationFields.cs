using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditTablePeopleReservationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "PeopleServices");

            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ToTime",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "Reservation",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PeopleId",
                table: "Reservation",
                newName: "IX_Reservation_CustomerId");

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ClerkServicesId = table.Column<int>(type: "int", nullable: false),
                    isSalonCustomer = table.Column<bool>(type: "bit", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_ClerkServices_ClerkServicesId",
                        column: x => x.ClerkServicesId,
                        principalTable: "ClerkServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ClerkServicesId",
                table: "ReservationDetails",
                column: "ClerkServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ReservationId",
                table: "ReservationDetails",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_People_CustomerId",
                table: "Reservation",
                column: "CustomerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_People_CustomerId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Reservation",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                newName: "IX_Reservation_PeopleId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FromTime",
                table: "Reservation",
                type: "time(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ToTime",
                table: "Reservation",
                type: "time(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "PeopleServices",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
