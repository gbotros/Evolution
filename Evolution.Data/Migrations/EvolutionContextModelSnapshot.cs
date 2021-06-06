﻿// <auto-generated />
using System;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Evolution.Data.Migrations
{
    [DbContext(typeof(EvolutionContext))]
    partial class EvolutionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evolution.Domain.AnimalAggregate.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChildrenCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<int>("FoodStorageCapacity")
                        .HasColumnType("int");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastAction")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextAction")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Steps")
                        .HasColumnType("int");

                    b.Property<int>("StoredFood")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Animals");
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

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Evolution.Domain.AnimalAggregate.Animal", b =>
                {
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
                });

            modelBuilder.Entity("Evolution.Domain.PlantAggregate.Plant", b =>
                {
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
                });
#pragma warning restore 612, 618
        }
    }
}
