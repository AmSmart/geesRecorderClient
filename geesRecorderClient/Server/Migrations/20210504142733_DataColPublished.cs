using Microsoft.EntityFrameworkCore.Migrations;

namespace geesRecorderClient.Server.Migrations
{
    public partial class DataColPublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Projects",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Projects");
        }
    }
}
