using Microsoft.EntityFrameworkCore.Migrations;

namespace geesRecorderClient.Server.Migrations
{
    public partial class ExtraSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockedRoute",
                table: "ServerState",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RouteLockActivated",
                table: "ServerState",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedRoute",
                table: "ServerState");

            migrationBuilder.DropColumn(
                name: "RouteLockActivated",
                table: "ServerState");
        }
    }
}
