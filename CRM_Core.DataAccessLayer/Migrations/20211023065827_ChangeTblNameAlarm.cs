using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class ChangeTblNameAlarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarm");

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlarmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlarmDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_RegisterDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    M_RegisterDate = table.Column<int>(type: "int", nullable: false),
                    F_EditDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_EditDate = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRepeatAlarm = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ToPersonelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminder_People_ToPersonelId",
                        column: x => x.ToPersonelId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_ToPersonelId",
                table: "Reminder",
                column: "ToPersonelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminder");

            migrationBuilder.CreateTable(
                name: "Alarm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlarmDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlarmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    F_EditDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_RegisterDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRepeatAlarm = table.Column<bool>(type: "bit", nullable: false),
                    M_EditDate = table.Column<int>(type: "int", nullable: false),
                    M_RegisterDate = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToPersonelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarm_People_ToPersonelId",
                        column: x => x.ToPersonelId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarm_ToPersonelId",
                table: "Alarm",
                column: "ToPersonelId");
        }
    }
}
