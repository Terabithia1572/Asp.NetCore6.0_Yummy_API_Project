using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyApi.WebApi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YummyEvents",
                columns: table => new
                {
                    YummyEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YummyEventTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YummyEventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YummyEventImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YummyEventStatus = table.Column<bool>(type: "bit", nullable: false),
                    YummyEventPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YummyEvents", x => x.YummyEventID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YummyEvents");
        }
    }
}
