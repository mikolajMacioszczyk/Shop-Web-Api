﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopApi.DAL;

namespace ShopApi.DAL.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20200918210814_AddIdColumnToFurnitureCountsTable")]
    partial class AddIdColumnToFurnitureCountsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("ShopApi.Models.Furnitures.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsLimited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnSale")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("CollectionItems");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.Furniture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CollectionId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Prize")
                        .HasColumnType("float");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("FurnitureItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Furniture");
                });

            modelBuilder.Entity("ShopApi.Models.Orders.FurnitureCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("FurnitureId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FurnitureId");

                    b.HasIndex("OrderId");

                    b.ToTable("FurnitureCounts");
                });

            modelBuilder.Entity("ShopApi.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfAdmission")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRealization")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrize")
                        .HasColumnType("float");

                    b.Property<int>("TotalWeight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ShopApi.Models.People.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("House")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AddressItems");
                });

            modelBuilder.Entity("ShopApi.Models.People.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("PeopleItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.FurnitureImplmentation.Chair", b =>
                {
                    b.HasBaseType("ShopApi.Models.Furnitures.Furniture");

                    b.HasDiscriminator().HasValue("Chair");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.FurnitureImplmentation.Corner", b =>
                {
                    b.HasBaseType("ShopApi.Models.Furnitures.Furniture");

                    b.Property<bool>("HaveHeadrests")
                        .HasColumnType("bit");

                    b.Property<bool>("HaveSleepMode")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Corner");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.FurnitureImplmentation.Sofa", b =>
                {
                    b.HasBaseType("ShopApi.Models.Furnitures.Furniture");

                    b.Property<bool>("HasSleepMode")
                        .HasColumnType("bit");

                    b.Property<int>("Pillows")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Sofa");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.FurnitureImplmentation.Table", b =>
                {
                    b.HasBaseType("ShopApi.Models.Furnitures.Furniture");

                    b.Property<bool>("IsFoldable")
                        .HasColumnType("bit");

                    b.Property<string>("Shape")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Table");
                });

            modelBuilder.Entity("ShopApi.Models.People.Customer", b =>
                {
                    b.HasBaseType("ShopApi.Models.People.Person");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("ShopApi.Models.People.Employee", b =>
                {
                    b.HasBaseType("ShopApi.Models.People.Person");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEmployment")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobTitles")
                        .HasColumnType("int");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("ShopApi.Models.Furnitures.Furniture", b =>
                {
                    b.HasOne("ShopApi.Models.Furnitures.Collection", "Collection")
                        .WithMany()
                        .HasForeignKey("CollectionId");

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("ShopApi.Models.Orders.FurnitureCount", b =>
                {
                    b.HasOne("ShopApi.Models.Furnitures.Furniture", "Furniture")
                        .WithMany()
                        .HasForeignKey("FurnitureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopApi.Models.Orders.Order", null)
                        .WithMany("Furnitures")
                        .HasForeignKey("OrderId");

                    b.Navigation("Furniture");
                });

            modelBuilder.Entity("ShopApi.Models.Orders.Order", b =>
                {
                    b.HasOne("ShopApi.Models.People.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ShopApi.Models.People.Person", b =>
                {
                    b.HasOne("ShopApi.Models.People.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ShopApi.Models.Orders.Order", b =>
                {
                    b.Navigation("Furnitures");
                });

            modelBuilder.Entity("ShopApi.Models.People.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
