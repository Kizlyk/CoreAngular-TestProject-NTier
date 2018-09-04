using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductDAL.Migrations
{
    public partial class AddConcurrencyCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Products",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Products");
        }
    }
}
