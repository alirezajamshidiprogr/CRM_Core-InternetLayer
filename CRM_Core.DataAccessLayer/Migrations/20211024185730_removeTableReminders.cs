using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class removeTableReminders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Reminder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
