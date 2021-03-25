using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindAPI.Migrations
{
    public partial class CourseAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "CourseCustomerCasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCustomerCasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCustomerCasts_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCustomerCasts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCustomerCasts_CourseId",
                table: "CourseCustomerCasts",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCustomerCasts_CustomerId",
                table: "CourseCustomerCasts",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCustomerCasts");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
