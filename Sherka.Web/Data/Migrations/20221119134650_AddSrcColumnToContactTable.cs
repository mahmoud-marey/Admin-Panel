using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sherka.Web.Data.Migrations
{
    public partial class AddSrcColumnToContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMapAddressSrc",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapAddressSrc",
                table: "Contacts");
        }
    }
}
