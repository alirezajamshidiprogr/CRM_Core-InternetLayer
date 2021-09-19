using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class editUserMenuForeigKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMenu_TBASMenu_TBASMenuId",
                table: "UserMenu");

            migrationBuilder.DropIndex(
                name: "IX_UserMenu_TBASMenuId",
                table: "UserMenu");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "UserMenu");

            migrationBuilder.AlterColumn<int>(
                name: "TBASMenuId",
                table: "UserMenu",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TBASMenuId",
                table: "UserMenu",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "UserMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_TBASMenuId",
                table: "UserMenu",
                column: "TBASMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenu_TBASMenu_TBASMenuId",
                table: "UserMenu",
                column: "TBASMenuId",
                principalTable: "TBASMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
