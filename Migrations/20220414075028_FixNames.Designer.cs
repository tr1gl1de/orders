﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Orders;

#nullable disable

namespace Orders.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220414075028_FixNames")]
    partial class FixNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Orders.Order.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AddressReceiver")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_receiver");

                    b.Property<string>("AddressSender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_sender");

                    b.Property<string>("CityReceiver")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city_receiver");

                    b.Property<string>("CitySender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city_sender");

                    b.Property<DateTime>("DateReceiving")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_receiving");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.ToTable("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
