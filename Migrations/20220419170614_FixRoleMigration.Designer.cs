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
    [Migration("20220419170614_FixRoleMigration")]
    partial class FixRoleMigration
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

            modelBuilder.Entity("Orders.User.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration_time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("refresh_tokens");
                });

            modelBuilder.Entity("Orders.User.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("15e5c330-7a98-4c33-8c21-4226bdcaf0f5"),
                            DisplayName = "Administrator",
                            Password = "$2a$11$sPvMVwoms.yYUj3MKVgJwuKx57MItFdd1LtADvqGqODFGLBf7ecHG",
                            Role = 2,
                            Username = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
