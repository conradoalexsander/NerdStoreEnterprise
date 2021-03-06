using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Catalogue.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                    IsActive = table.Column<string>(type: "varchar(100)", nullable: true),
                    Value = table.Column<string>(type: "varchar(100)", nullable: true),
                    RegistryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "varchar(250)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
