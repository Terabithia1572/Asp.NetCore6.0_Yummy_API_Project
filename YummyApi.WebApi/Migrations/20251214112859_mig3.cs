using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyApi.WebApi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupReservationEmail",
                table: "GroupReservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupReservationPersonCount",
                table: "GroupReservations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupReservationEmail",
                table: "GroupReservations");

            migrationBuilder.DropColumn(
                name: "GroupReservationPersonCount",
                table: "GroupReservations");
        }
    }
}
