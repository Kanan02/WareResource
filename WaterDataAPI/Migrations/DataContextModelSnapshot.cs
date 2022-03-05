﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WaterDataAPI.Data;

#nullable disable

namespace WaterDataAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WaterDataAPI.Models.Concrete.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("CriticalWaterLevel")
                        .HasColumnType("float");

                    b.Property<double>("CurrentWaterHeight")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PollutionLevel")
                        .HasColumnType("float");

                    b.Property<double>("StandardWaterHeight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Channels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CriticalWaterLevel = 0.0,
                            CurrentWaterHeight = 0.0,
                            Name = "River1",
                            PollutionLevel = 0.0,
                            StandardWaterHeight = 0.0
                        },
                        new
                        {
                            Id = 2,
                            CriticalWaterLevel = 0.0,
                            CurrentWaterHeight = 0.0,
                            Name = "River2",
                            PollutionLevel = 0.0,
                            StandardWaterHeight = 0.0
                        });
                });

            modelBuilder.Entity("WaterDataAPI.Models.Concrete.GroundWaterReservoir", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("CurrentWaterLevel")
                        .HasColumnType("float");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PollutionLevel")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GroundWaterReservoirs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentWaterLevel = 200.0,
                            Height = 220.0,
                            Length = 0.0,
                            Name = "GS1",
                            PollutionLevel = 8.0,
                            Width = 100.0
                        },
                        new
                        {
                            Id = 2,
                            CurrentWaterLevel = 220.0,
                            Height = 320.0,
                            Length = 0.0,
                            Name = "GS2",
                            PollutionLevel = 12.0,
                            Width = 170.0
                        });
                });

            modelBuilder.Entity("WaterDataAPI.Models.Concrete.RainWaterReservoir", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("CurrentWaterLevel")
                        .HasColumnType("float");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PollutionLevel")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RainWaterReservoirs");
                });

            modelBuilder.Entity("WaterDataAPI.Models.Concrete.WaterReservoir", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PollutionLevel")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WaterReservoirs");
                });
#pragma warning restore 612, 618
        }
    }
}