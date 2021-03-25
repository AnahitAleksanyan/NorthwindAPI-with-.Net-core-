using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindAPI.Migrations
{
    public partial class UserAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbIUser",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Salt = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    CustomerId = table.Column<string>(type: "nchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbIUser", x => x.Username);
                    table.ForeignKey(
                        name: "FK_tbIUser_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbIUser");
        }
    }
}
