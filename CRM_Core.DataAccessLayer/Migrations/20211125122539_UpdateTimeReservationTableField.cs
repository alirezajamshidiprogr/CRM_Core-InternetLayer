using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class UpdateTimeReservationTableField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reservation");
        }
    }
}
