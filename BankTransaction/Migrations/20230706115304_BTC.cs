using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransaction.Migrations
{
    public partial class BTC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankTransaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfSender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfReciever = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    governorate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountInDollar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountInEuro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfTransaction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfReciever = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankTransaction", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankTransaction");
        }
    }
}
