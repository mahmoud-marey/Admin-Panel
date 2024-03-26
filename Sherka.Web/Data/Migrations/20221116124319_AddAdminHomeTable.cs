using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sherka.Web.Data.Migrations
{
    public partial class AddAdminHomeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminHome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SiteDiscription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HeaderDiscription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderBackgroundUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminHome", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminHome");
        }
    }
}
