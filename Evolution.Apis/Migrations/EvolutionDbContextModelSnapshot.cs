﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evolution.Apis.Migrations
{
    [DbContext(typeof(EvolutionDbContext))]
    internal class EvolutionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evolution.Entities.AnimalBlueprint", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime2");

                b.Property<int>("ChildrenCount")
                    .HasColumnType("int");

                b.Property<DateTime?>("DeathDate")
                    .HasColumnType("datetime2");

                b.Property<int>("Energy")
                    .HasColumnType("int");

                b.Property<bool>("IsAlive")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid?>("ParentId")
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("Speed")
                    .HasColumnType("int");

                b.Property<int>("Steps")
                    .HasColumnType("int");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("datetime2");

                b.Property<int>("Weight")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Animals");
            });

            modelBuilder.Entity("Evolution.Entities.PlantBlueprint", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DeathDate")
                    .HasColumnType("datetime2");

                b.Property<int>("GrowthAmount")
                    .HasColumnType("int");

                b.Property<bool>("IsAlive")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid?>("ParentId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("datetime2");

                b.Property<int>("Weight")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Plants");
            });

            modelBuilder.Entity("Evolution.Entities.AnimalBlueprint", b =>
            {
                b.OwnsOne("Evolution.Entities.LocationBlueprint", "Location", b1 =>
                {
                    b1.Property<Guid>("AnimalBlueprintId")
                        .HasColumnType("uniqueidentifier");

                    b1.Property<int>("X")
                        .HasColumnType("int");

                    b1.Property<int>("Y")
                        .HasColumnType("int");

                    b1.HasKey("AnimalBlueprintId");

                    b1.ToTable("Animals");

                    b1.WithOwner()
                        .HasForeignKey("AnimalBlueprintId");
                });
            });

            modelBuilder.Entity("Evolution.Entities.PlantBlueprint", b =>
            {
                b.OwnsOne("Evolution.Entities.LocationBlueprint", "Location", b1 =>
                {
                    b1.Property<Guid>("PlantBlueprintId")
                        .HasColumnType("uniqueidentifier");

                    b1.Property<int>("X")
                        .HasColumnType("int");

                    b1.Property<int>("Y")
                        .HasColumnType("int");

                    b1.HasKey("PlantBlueprintId");

                    b1.ToTable("Plants");

                    b1.WithOwner()
                        .HasForeignKey("PlantBlueprintId");
                });
            });
#pragma warning restore 612, 618
        }
    }
}