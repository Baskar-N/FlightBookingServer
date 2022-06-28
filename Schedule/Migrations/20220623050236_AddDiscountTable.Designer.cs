﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleApiService.Services;

namespace ScheduleApiService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220623050236_AddDiscountTable")]
    partial class AddDiscountTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SharedModels.Models.Airline", b =>
                {
                    b.Property<int>("AirlineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContactNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AirlineId");

                    b.ToTable("Airline");
                });

            modelBuilder.Entity("SharedModels.Models.Discount", b =>
                {
                    b.Property<int>("DiscountRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<string>("DiscountCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("DiscountRecId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("SharedModels.Models.Location", b =>
                {
                    b.Property<int>("LocationRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationRecId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("SharedModels.Models.Meal", b =>
                {
                    b.Property<int>("MealTypeRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MealType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MealTypeRecId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("SharedModels.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<int>("Bcs")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDateTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("FromPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentUsed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MealTypeRecId")
                        .HasColumnType("int");

                    b.Property<int>("NonBcs")
                        .HasColumnType("int");

                    b.Property<int>("Rows")
                        .HasColumnType("int");

                    b.Property<int>("ScheduledDaysRecId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDateTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<double>("TicketCost")
                        .HasColumnType("float");

                    b.Property<string>("ToPlace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduleRecId");

                    b.HasIndex("AirlineId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("SharedModels.Models.ScheduledDays", b =>
                {
                    b.Property<int>("ScheduledDaysRecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ScheduledType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduledDaysRecId");

                    b.ToTable("ScheduledDaysType");
                });

            modelBuilder.Entity("SharedModels.Models.Schedule", b =>
                {
                    b.HasOne("SharedModels.Models.Airline", "Airline")
                        .WithMany("Schedules")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
