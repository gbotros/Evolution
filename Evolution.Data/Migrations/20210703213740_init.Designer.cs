﻿// <auto-generated />
using System;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Evolution.Data.Migrations
{
    [DbContext(typeof(EvolutionContext))]
    [Migration("20210703213740_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evolution.Domain.AnimalAggregate.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AdulthoodAge")
                        .HasColumnType("int");

                    b.Property<int>("ChildrenCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<double>("Energy")
                        .HasColumnType("float");

                    b.Property<int>("FoodStorageCapacity")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdult")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastAction")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastChildAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("MaxEnergy")
                        .HasColumnType("float");

                    b.Property<int>("MaxFoodStorageCapacity")
                        .HasColumnType("int");

                    b.Property<int>("MaxSense")
                        .HasColumnType("int");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("float");

                    b.Property<double>("MinEnergy")
                        .HasColumnType("float");

                    b.Property<int>("MinFoodStorageCapacity")
                        .HasColumnType("int");

                    b.Property<int>("MinSense")
                        .HasColumnType("int");

                    b.Property<double>("MinSpeed")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextAction")
                        .HasColumnType("datetime2");

                    b.Property<int>("OneFoodToEnergy")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sense")
                        .HasColumnType("int");

                    b.Property<long>("SenseMutationAmplitude")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.Property<long>("SpeedMutationAmplitude")
                        .HasColumnType("bigint");

                    b.Property<int>("Steps")
                        .HasColumnType("int");

                    b.Property<int>("StoredFood")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Evolution.Domain.GameSettingsAggregate.GameSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("GameSettings");
                });

            modelBuilder.Entity("Evolution.Domain.PlantAggregate.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GrowthAmount")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Evolution.Domain.AnimalAggregate.Animal", b =>
                {
                    b.HasOne("Evolution.Domain.GameSettingsAggregate.GameSettings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");

                    b.OwnsOne("Evolution.Domain.Common.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("AnimalId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Column")
                                .HasColumnType("int")
                                .HasColumnName("Column");

                            b1.Property<int>("Row")
                                .HasColumnType("int")
                                .HasColumnName("Row");

                            b1.HasKey("AnimalId");

                            b1.ToTable("Animals");

                            b1.WithOwner()
                                .HasForeignKey("AnimalId");
                        });

                    b.Navigation("Location");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Evolution.Domain.GameSettingsAggregate.GameSettings", b =>
                {
                    b.OwnsOne("Evolution.Domain.GameSettingsAggregate.AnimalDefaults", "AnimalDefaults", b1 =>
                        {
                            b1.Property<Guid>("GameSettingsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("AdulthoodAge")
                                .HasColumnType("int");

                            b1.Property<double>("Energy")
                                .HasColumnType("float");

                            b1.Property<int>("FoodStorageCapacity")
                                .HasColumnType("int");

                            b1.Property<double>("MaxEnergy")
                                .HasColumnType("float");

                            b1.Property<int>("MaxFoodStorageCapacity")
                                .HasColumnType("int");

                            b1.Property<int>("MaxSense")
                                .HasColumnType("int");

                            b1.Property<double>("MaxSpeed")
                                .HasColumnType("float");

                            b1.Property<double>("MinEnergy")
                                .HasColumnType("float");

                            b1.Property<int>("MinFoodStorageCapacity")
                                .HasColumnType("int");

                            b1.Property<int>("MinSense")
                                .HasColumnType("int");

                            b1.Property<double>("MinSpeed")
                                .HasColumnType("float");

                            b1.Property<int>("OneFoodToEnergy")
                                .HasColumnType("int");

                            b1.Property<int>("Sense")
                                .HasColumnType("int");

                            b1.Property<long>("SenseMutationAmplitude")
                                .HasColumnType("bigint");

                            b1.Property<double>("Speed")
                                .HasColumnType("float");

                            b1.Property<long>("SpeedMutationAmplitude")
                                .HasColumnType("bigint");

                            b1.HasKey("GameSettingsId");

                            b1.ToTable("GameSettings");

                            b1.WithOwner()
                                .HasForeignKey("GameSettingsId");
                        });

                    b.OwnsOne("Evolution.Domain.GameSettingsAggregate.WorldSize", "WorldSize", b1 =>
                        {
                            b1.Property<Guid>("GameSettingsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("GameSettingsId");

                            b1.ToTable("GameSettings");

                            b1.WithOwner()
                                .HasForeignKey("GameSettingsId");
                        });

                    b.Navigation("AnimalDefaults");

                    b.Navigation("WorldSize");
                });

            modelBuilder.Entity("Evolution.Domain.PlantAggregate.Plant", b =>
                {
                    b.HasOne("Evolution.Domain.GameSettingsAggregate.GameSettings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");

                    b.OwnsOne("Evolution.Domain.Common.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("PlantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Column")
                                .HasColumnType("int")
                                .HasColumnName("Column");

                            b1.Property<int>("Row")
                                .HasColumnType("int")
                                .HasColumnName("Row");

                            b1.HasKey("PlantId");

                            b1.ToTable("Plants");

                            b1.WithOwner()
                                .HasForeignKey("PlantId");
                        });

                    b.Navigation("Location");

                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}