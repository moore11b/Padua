using Microsoft.EntityFrameworkCore.Migrations;

namespace LabW11Authentication.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ApplicationUser_UserId1",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId1",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ApplicationUser_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ApplicationUser_UserId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId1",
                table: "Devices",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ApplicationUser_UserId1",
                table: "Devices",
                column: "UserId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
