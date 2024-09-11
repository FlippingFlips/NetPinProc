﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetPinProc.Game.Sqlite;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    [DbContext(typeof(NetProcDbContext))]
    [Migration("20240911181730_AddedDataToPlayer")]
    partial class AddedDataToPlayer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("NetPinProc.Domain.Data.Adjustment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OptionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Options")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SubMenuName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ValueDefault")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Adjustments", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.Data.Audit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.CoilConfigFileEntry", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<uint>("NumberPROC")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Polarity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PulseTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReturnWire")
                        .HasColumnType("TEXT");

                    b.Property<int>("Search")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<byte?>("Voltage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VoltageWire")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Number");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Coils", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.GIConfigFileEntry", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Polarity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Number");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("GI", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.LampConfigFileEntry", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Polarity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Number");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Lamps", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.LedConfigFileEntry", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<uint>("NumberPROC")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Polarity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Number");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Leds", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.Lpd8806ConfigFileEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<uint>("First")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Last")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("BoardId", "Index")
                        .IsUnique();

                    b.ToTable("Lpd8806Leds", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.ServoConfigFileEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<int>("MinValue")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("Voltage")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("BoardId", "Index")
                        .IsUnique();

                    b.ToTable("Servos", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.StepperConfigFileEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStepper1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Speed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StopSwitch")
                        .HasColumnType("TEXT");

                    b.Property<byte?>("Voltage")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("BoardId", "IsStepper1")
                        .IsUnique();

                    b.ToTable("Steppers", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.SwitchConfigFileEntry", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GroundWire")
                        .HasColumnType("TEXT");

                    b.Property<string>("InputWire")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("SearchReset")
                        .HasColumnType("TEXT");

                    b.Property<string>("SearchStop")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Number");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Switches", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Domain.MachineConfig.WS281xConfigFileEntry", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Conn")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<uint>("First")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Last")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("BoardId", "Index")
                        .IsUnique();

                    b.ToTable("WS281xLeds", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.BallPlayed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Ball")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScoreId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Time")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ScoreId");

                    b.ToTable("BallsPlayed");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.ColorSet", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("HtmlCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Colors");

                    b.HasData(
                        new
                        {
                            Name = "BLK",
                            HtmlCode = "010101"
                        },
                        new
                        {
                            Name = "WHT",
                            HtmlCode = "FEFEFE"
                        },
                        new
                        {
                            Name = "BLU",
                            HtmlCode = "0155FE"
                        },
                        new
                        {
                            Name = "RED",
                            HtmlCode = "FF0000"
                        },
                        new
                        {
                            Name = "YEL",
                            HtmlCode = "EDFE01"
                        },
                        new
                        {
                            Name = "ORG",
                            HtmlCode = "FE9001"
                        },
                        new
                        {
                            Name = "GRN",
                            HtmlCode = "3CDF13"
                        },
                        new
                        {
                            Name = "VIO",
                            HtmlCode = "A913DF"
                        });
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.GamePlayed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adjustments")
                        .HasColumnType("TEXT");

                    b.Property<byte>("BallsPerGame")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Ended")
                        .HasColumnType("TEXT");

                    b.Property<double>("GameTime")
                        .HasColumnType("DOUBLE(18, 2)");

                    b.Property<DateTime>("Started")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GamesPlayed");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DisplayMonitor")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MachineType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumBalls")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Media", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Data = new byte[] { 60, 63, 120, 109, 108, 32, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 48, 34, 32, 101, 110, 99, 111, 100, 105, 110, 103, 61, 34, 85, 84, 70, 45, 56, 34, 32, 115, 116, 97, 110, 100, 97, 108, 111, 110, 101, 61, 34, 110, 111, 34, 63, 62, 13, 10, 60, 33, 45, 45, 32, 67, 114, 101, 97, 116, 101, 100, 32, 119, 105, 116, 104, 32, 73, 110, 107, 115, 99, 97, 112, 101, 32, 40, 104, 116, 116, 112, 58, 47, 47, 119, 119, 119, 46, 105, 110, 107, 115, 99, 97, 112, 101, 46, 111, 114, 103, 47, 41, 32, 45, 45, 62, 13, 10, 13, 10, 60, 115, 118, 103, 13, 10, 32, 32, 32, 119, 105, 100, 116, 104, 61, 34, 53, 49, 52, 46, 51, 52, 57, 57, 56, 109, 109, 34, 13, 10, 32, 32, 32, 104, 101, 105, 103, 104, 116, 61, 34, 49, 48, 54, 54, 46, 56, 109, 109, 34, 13, 10, 32, 32, 32, 118, 105, 101, 119, 66, 111, 120, 61, 34, 48, 32, 48, 32, 53, 49, 52, 46, 51, 52, 57, 57, 56, 32, 49, 48, 54, 54, 46, 56, 48, 48, 49, 34, 13, 10, 32, 32, 32, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 49, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 115, 118, 103, 51, 51, 51, 56, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 50, 46, 50, 32, 40, 55, 51, 50, 97, 48, 49, 100, 97, 54, 51, 44, 32, 50, 48, 50, 50, 45, 49, 50, 45, 48, 57, 41, 34, 13, 10, 32, 32, 32, 115, 111, 100, 105, 112, 111, 100, 105, 58, 100, 111, 99, 110, 97, 109, 101, 61, 34, 112, 108, 97, 121, 102, 105, 101, 108, 100, 116, 101, 109, 112, 108, 97, 116, 101, 46, 115, 118, 103, 34, 13, 10, 32, 32, 32, 120, 109, 108, 58, 115, 112, 97, 99, 101, 61, 34, 112, 114, 101, 115, 101, 114, 118, 101, 34, 13, 10, 32, 32, 32, 120, 109, 108, 110, 115, 58, 105, 110, 107, 115, 99, 97, 112, 101, 61, 34, 104, 116, 116, 112, 58, 47, 47, 119, 119, 119, 46, 105, 110, 107, 115, 99, 97, 112, 101, 46, 111, 114, 103, 47, 110, 97, 109, 101, 115, 112, 97, 99, 101, 115, 47, 105, 110, 107, 115, 99, 97, 112, 101, 34, 13, 10, 32, 32, 32, 120, 109, 108, 110, 115, 58, 115, 111, 100, 105, 112, 111, 100, 105, 61, 34, 104, 116, 116, 112, 58, 47, 47, 115, 111, 100, 105, 112, 111, 100, 105, 46, 115, 111, 117, 114, 99, 101, 102, 111, 114, 103, 101, 46, 110, 101, 116, 47, 68, 84, 68, 47, 115, 111, 100, 105, 112, 111, 100, 105, 45, 48, 46, 100, 116, 100, 34, 13, 10, 32, 32, 32, 120, 109, 108, 110, 115, 61, 34, 104, 116, 116, 112, 58, 47, 47, 119, 119, 119, 46, 119, 51, 46, 111, 114, 103, 47, 50, 48, 48, 48, 47, 115, 118, 103, 34, 13, 10, 32, 32, 32, 120, 109, 108, 110, 115, 58, 115, 118, 103, 61, 34, 104, 116, 116, 112, 58, 47, 47, 119, 119, 119, 46, 119, 51, 46, 111, 114, 103, 47, 50, 48, 48, 48, 47, 115, 118, 103, 34, 62, 13, 10, 9, 60, 115, 111, 100, 105, 112, 111, 100, 105, 58, 110, 97, 109, 101, 100, 118, 105, 101, 119, 13, 10, 32, 32, 32, 105, 100, 61, 34, 110, 97, 109, 101, 100, 118, 105, 101, 119, 51, 51, 52, 48, 34, 13, 10, 32, 32, 32, 112, 97, 103, 101, 99, 111, 108, 111, 114, 61, 34, 35, 102, 102, 102, 102, 102, 102, 34, 13, 10, 32, 32, 32, 98, 111, 114, 100, 101, 114, 99, 111, 108, 111, 114, 61, 34, 35, 48, 48, 48, 48, 48, 48, 34, 13, 10, 32, 32, 32, 98, 111, 114, 100, 101, 114, 111, 112, 97, 99, 105, 116, 121, 61, 34, 48, 46, 50, 53, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 115, 104, 111, 119, 112, 97, 103, 101, 115, 104, 97, 100, 111, 119, 61, 34, 50, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 112, 97, 103, 101, 111, 112, 97, 99, 105, 116, 121, 61, 34, 48, 46, 48, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 112, 97, 103, 101, 99, 104, 101, 99, 107, 101, 114, 98, 111, 97, 114, 100, 61, 34, 48, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 100, 101, 115, 107, 99, 111, 108, 111, 114, 61, 34, 35, 100, 49, 100, 49, 100, 49, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 100, 111, 99, 117, 109, 101, 110, 116, 45, 117, 110, 105, 116, 115, 61, 34, 109, 109, 34, 13, 10, 32, 32, 32, 115, 104, 111, 119, 103, 114, 105, 100, 61, 34, 102, 97, 108, 115, 101, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 122, 111, 111, 109, 61, 34, 48, 46, 49, 51, 48, 53, 53, 50, 54, 50, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 99, 120, 61, 34, 53, 48, 57, 46, 51, 55, 51, 49, 55, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 99, 121, 61, 34, 49, 52, 55, 48, 46, 54, 55, 49, 52, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 119, 105, 110, 100, 111, 119, 45, 119, 105, 100, 116, 104, 61, 34, 49, 57, 50, 48, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 119, 105, 110, 100, 111, 119, 45, 104, 101, 105, 103, 104, 116, 61, 34, 49, 48, 48, 57, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 119, 105, 110, 100, 111, 119, 45, 120, 61, 34, 45, 56, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 119, 105, 110, 100, 111, 119, 45, 121, 61, 34, 45, 56, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 119, 105, 110, 100, 111, 119, 45, 109, 97, 120, 105, 109, 105, 122, 101, 100, 61, 34, 49, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 99, 117, 114, 114, 101, 110, 116, 45, 108, 97, 121, 101, 114, 61, 34, 108, 97, 121, 101, 114, 55, 34, 32, 47, 62, 60, 100, 101, 102, 115, 13, 10, 32, 32, 32, 105, 100, 61, 34, 100, 101, 102, 115, 51, 51, 51, 53, 34, 32, 47, 62, 13, 10, 9, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 55, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 71, 73, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 52, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 83, 87, 73, 84, 67, 72, 69, 83, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 53, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 83, 84, 69, 80, 80, 69, 82, 83, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 54, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 83, 69, 82, 86, 79, 83, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 76, 69, 68, 83, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 49, 34, 13, 10, 32, 32, 32, 116, 114, 97, 110, 115, 102, 111, 114, 109, 61, 34, 116, 114, 97, 110, 115, 108, 97, 116, 101, 40, 48, 44, 45, 54, 46, 51, 56, 51, 50, 54, 57, 53, 101, 45, 53, 41, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 50, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 76, 65, 77, 80, 83, 34, 13, 10, 32, 32, 32, 116, 114, 97, 110, 115, 102, 111, 114, 109, 61, 34, 116, 114, 97, 110, 115, 108, 97, 116, 101, 40, 48, 44, 45, 54, 46, 51, 56, 51, 50, 54, 57, 53, 101, 45, 53, 41, 34, 32, 47, 62, 60, 103, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 103, 114, 111, 117, 112, 109, 111, 100, 101, 61, 34, 108, 97, 121, 101, 114, 34, 13, 10, 32, 32, 32, 105, 100, 61, 34, 108, 97, 121, 101, 114, 51, 34, 13, 10, 32, 32, 32, 105, 110, 107, 115, 99, 97, 112, 101, 58, 108, 97, 98, 101, 108, 61, 34, 68, 82, 73, 86, 69, 82, 83, 34, 13, 10, 32, 32, 32, 116, 114, 97, 110, 115, 102, 111, 114, 109, 61, 34, 116, 114, 97, 110, 115, 108, 97, 116, 101, 40, 48, 44, 45, 54, 46, 51, 56, 51, 50, 54, 57, 53, 101, 45, 53, 41, 34, 32, 47, 62, 60, 47, 115, 118, 103, 62, 13, 10 },
                            MimeType = "application/xml+svg",
                            Name = "playfieldtemplate.svg",
                            Size = 1893L,
                            Tags = "playfield,svg"
                        });
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Part", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Material")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PartNo")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PartType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Shape")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("XPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("YPos")
                        .HasColumnType("REAL");

                    b.Property<double?>("ZPos")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.ToTable("Parts", (string)null);
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Default")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Initials")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("ExtraBallsPlayed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePlayedId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Points")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GamePlayedId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.BallPlayed", b =>
                {
                    b.HasOne("NetPinProc.Game.Sqlite.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetPinProc.Game.Sqlite.Model.Score", "Score")
                        .WithMany("BallsPlayed")
                        .HasForeignKey("ScoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Score");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Score", b =>
                {
                    b.HasOne("NetPinProc.Game.Sqlite.Model.GamePlayed", "GamePlayed")
                        .WithMany("Scores")
                        .HasForeignKey("GamePlayedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetPinProc.Game.Sqlite.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GamePlayed");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.GamePlayed", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("NetPinProc.Game.Sqlite.Model.Score", b =>
                {
                    b.Navigation("BallsPlayed");
                });
#pragma warning restore 612, 618
        }
    }
}
