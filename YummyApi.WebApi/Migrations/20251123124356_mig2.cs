using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyApi.WebApi.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupReservations",
                columns: table => new
                {
                    GroupReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupReservationTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupReservationLastProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupReservationPriority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupReservationDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupReservationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupReservations", x => x.GroupReservationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupReservations");
        }
    }
}
