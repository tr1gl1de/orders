using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    public partial class AddRoleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "display_name", "password", "role", "username" },
                values: new object[] { new Guid("90c7893f-3ef6-4f3c-a5e0-4d8b91f4735a"), "Administrator", "$2a$11$hv7tEoCmFQsoz87Qq3.fcuOGrapltrBRj.TWmU3ptakOR3w.5K1.K", 2, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("90c7893f-3ef6-4f3c-a5e0-4d8b91f4735a"));

            migrationBuilder.DropColumn(
                name: "role",
                table: "users");
        }
    }
}
