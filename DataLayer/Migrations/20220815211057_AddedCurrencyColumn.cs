using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddedCurrencyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Offers",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(5)");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CurrencyID",
                table: "Items",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Currencies_CurrencyID",
                table: "Items",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "CurrencyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Currencies_CurrencyID",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Items_CurrencyID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Items");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Offers",
                type: "float(5)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
