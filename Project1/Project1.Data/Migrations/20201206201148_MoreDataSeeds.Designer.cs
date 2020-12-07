﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project1.Data.Model;

namespace Project1.Data.Migrations
{
    [DbContext(typeof(P1DbContext))]
    [Migration("20201206201148_MoreDataSeeds")]
    partial class MoreDataSeeds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Project1.Data.Model.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Sam",
                            LastName = "Alito"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Ruth",
                            LastName = "Ginsburg"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Antonin",
                            LastName = "Scalia"
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.InventoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("Inventory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductId = 1,
                            Quantity = 150,
                            StoreId = 1
                        },
                        new
                        {
                            Id = 2,
                            ProductId = 2,
                            Quantity = 200,
                            StoreId = 1
                        },
                        new
                        {
                            Id = 3,
                            ProductId = 3,
                            Quantity = 100,
                            StoreId = 2
                        },
                        new
                        {
                            Id = 4,
                            ProductId = 1,
                            Quantity = 150,
                            StoreId = 2
                        },
                        new
                        {
                            Id = 5,
                            ProductId = 2,
                            Quantity = 250,
                            StoreId = 3
                        },
                        new
                        {
                            Id = 6,
                            ProductId = 3,
                            Quantity = 100,
                            StoreId = 3
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            StoreId = 1,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            StoreId = 1,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 3,
                            StoreId = 2,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 1,
                            StoreId = 2,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CustomerId = 2,
                            StoreId = 3,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CustomerId = 3,
                            StoreId = 3,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.OrderItemsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 15
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 3,
                            Quantity = 5
                        },
                        new
                        {
                            Id = 4,
                            OrderId = 6,
                            ProductId = 1,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 5,
                            OrderId = 3,
                            ProductId = 2,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 6,
                            OrderId = 3,
                            ProductId = 3,
                            Quantity = 25
                        },
                        new
                        {
                            Id = 7,
                            OrderId = 4,
                            ProductId = 1,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 8,
                            OrderId = 5,
                            ProductId = 2,
                            Quantity = 15
                        },
                        new
                        {
                            Id = 9,
                            OrderId = 6,
                            ProductId = 3,
                            Quantity = 5
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ__Product")
                        .IsUnique();

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Banana",
                            Price = 1.99
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bread",
                            Price = 2.9900000000000002
                        },
                        new
                        {
                            Id = 3,
                            Name = "Propane",
                            Price = 30.989999999999998
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.StoreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Location" }, "UQ__Store")
                        .IsUnique();

                    b.ToTable("Store");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Texas",
                            Name = "StoreA"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Arizona",
                            Name = "StoreB"
                        },
                        new
                        {
                            Id = 3,
                            Location = "California",
                            Name = "StoreC"
                        });
                });

            modelBuilder.Entity("Project1.Data.Model.InventoryEntity", b =>
                {
                    b.HasOne("Project1.Data.Model.ProductEntity", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project1.Data.Model.StoreEntity", "Store")
                        .WithMany("Inventories")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Project1.Data.Model.OrderEntity", b =>
                {
                    b.HasOne("Project1.Data.Model.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project1.Data.Model.StoreEntity", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Project1.Data.Model.OrderItemsEntity", b =>
                {
                    b.HasOne("Project1.Data.Model.OrderEntity", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project1.Data.Model.ProductEntity", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Project1.Data.Model.CustomerEntity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Project1.Data.Model.OrderEntity", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Project1.Data.Model.ProductEntity", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Project1.Data.Model.StoreEntity", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
