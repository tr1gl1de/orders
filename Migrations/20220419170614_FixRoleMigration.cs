using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    public partial class FixRoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("90c7893f-3ef6-4f3c-a5e0-4d8b91f4735a"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "display_name", "password", "role", "username" },
                values: new object[] { new Guid("15e5c330-7a98-4c33-8c21-4226bdcaf0f5"), "Administrator", "$2a$11$sPvMVwoms.yYUj3MKVgJwuKx57MItFdd1LtADvqGqODFGLBf7ecHG", 2, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("15e5c330-7a98-4c33-8c21-4226bdcaf0f5"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "display_name", "password", "role", "username" },
                values: new object[] { new Guid("90c7893f-3ef6-4f3c-a5e0-4d8b91f4735a"), "Administrator", "$2a$11$hv7tEoCmFQsoz87Qq3.fcuOGrapltrBRj.TWmU3ptakOR3w.5K1.K", 2, "Administrator" });
        }
    }
}
