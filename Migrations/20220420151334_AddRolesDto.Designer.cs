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
    [Migration("20220420151334_AddRolesDto")]
    partial class AddRolesDto
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

                    b.Property<int[]>("Roles")
                        .IsRequired()
                        .HasColumnType("integer[]")
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
                            Id = new Guid("cc2fcbb5-8ce2-4cd3-bf53-7614253b5bd0"),
                            DisplayName = "Administrator",
                            Password = "$2a$11$E7rZNnsO1Rf42q8BXF2wJOLozNrgDQs9DNbupAfzxLSCfBp3gLBFK",
                            Roles = new[] { 2 },
                            Username = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
