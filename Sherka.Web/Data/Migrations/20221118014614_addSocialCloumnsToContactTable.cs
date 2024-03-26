using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sherka.Web.Data.Migrations
{
    public partial class addSocialCloumnsToContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterLink",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TwitterLink",
                table: "Contacts");
        }
    }
}
