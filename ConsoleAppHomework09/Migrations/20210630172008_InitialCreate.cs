using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleAppHomework09.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    orderDetails_OrderName = table.Column<string>(type: "TEXT", nullable: true),
                    orderDetails_OrderUserName = table.Column<string>(type: "TEXT", nullable: true),
                    orderDetails_OrderAmount = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
