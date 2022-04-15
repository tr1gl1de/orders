using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Migrations
{
    public partial class FixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "orders",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DateReceiving",
                table: "orders",
                newName: "date_receiving");

            migrationBuilder.RenameColumn(
                name: "CitySender",
                table: "orders",
                newName: "city_sender");

            migrationBuilder.RenameColumn(
                name: "CityReceiver",
                table: "orders",
                newName: "city_receiver");

            migrationBuilder.RenameColumn(
                name: "AdressSender",
                table: "orders",
                newName: "address_sender");

            migrationBuilder.RenameColumn(
                name: "AdressReceiver",
                table: "orders",
                newName: "address_receiver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_receiving",
                table: "orders",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Orders",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "date_receiving",
                table: "Orders",
                newName: "DateReceiving");

            migrationBuilder.RenameColumn(
                name: "city_sender",
                table: "Orders",
                newName: "CitySender");

            migrationBuilder.RenameColumn(
                name: "city_receiver",
                table: "Orders",
                newName: "CityReceiver");

            migrationBuilder.RenameColumn(
                name: "address_sender",
                table: "Orders",
                newName: "AdressSender");

            migrationBuilder.RenameColumn(
                name: "address_receiver",
                table: "Orders",
                newName: "AdressReceiver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReceiving",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");
        }
    }
}
