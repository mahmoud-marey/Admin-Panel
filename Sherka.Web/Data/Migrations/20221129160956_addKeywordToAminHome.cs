using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sherka.Web.Data.Migrations
{
    public partial class addKeywordToAminHome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "AdminHome",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "AdminHome");
        }
    }
}
