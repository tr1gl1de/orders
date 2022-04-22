using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    public partial class AddRolesDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("15e5c330-7a98-4c33-8c21-4226bdcaf0f5"));

            migrationBuilder.AlterColumn<int[]>(
                name: "role",
                table: "users",
                type: "integer[]",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "display_name", "password", "role", "username" },
                values: new object[] { new Guid("cc2fcbb5-8ce2-4cd3-bf53-7614253b5bd0"), "Administrator", "$2a$11$E7rZNnsO1Rf42q8BXF2wJOLozNrgDQs9DNbupAfzxLSCfBp3gLBFK", new[] { 2 }, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("cc2fcbb5-8ce2-4cd3-bf53-7614253b5bd0"));

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int[]),
                oldType: "integer[]");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "display_name", "password", "role", "username" },
                values: new object[] { new Guid("15e5c330-7a98-4c33-8c21-4226bdcaf0f5"), "Administrator", "$2a$11$sPvMVwoms.yYUj3MKVgJwuKx57MItFdd1LtADvqGqODFGLBf7ecHG", 2, "admin" });
        }
    }
}
