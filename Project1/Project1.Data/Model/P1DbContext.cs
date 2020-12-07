using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.Data.Model
{
    public class P1DbContext : DbContext
    {
        public P1DbContext(DbContextOptions<P1DbContext> options) : base(options)
        { 
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<InventoryEntity> Inventories { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemsEntity> OrderItems { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StoreEntity> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.FirstName)
                    .IsRequired();
                entity.Property(e => e.LastName)
                    .IsRequired();

            });

            modelBuilder.Entity<InventoryEntity>(entity =>
            {
                entity.ToTable("Inventory");
                entity.HasOne(e => e.Product)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.ProductId);
                entity.HasOne(e => e.Store)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.StoreId);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GetDate()")
                    .IsRequired();
                entity.HasOne(e => e.Store)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.StoreId);
                entity.HasOne(e => e.Customer)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.CustomerId);
            });

            modelBuilder.Entity<OrderItemsEntity>(entity =>
            {
                entity.ToTable("OrderItems");

                entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId);

            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Name, "UQ__Product")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<StoreEntity>(entity =>
            {
                entity.ToTable("Store");

                entity.HasIndex(e => e.Location, "UQ__Store")
                    .IsUnique();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });
            modelBuilder.Entity<ProductEntity>()
                .HasData(new ProductEntity[]
                {
                    new ProductEntity { Id = 1, Name = "Banana", Price = 1.99 },
                    new ProductEntity { Id = 2, Name = "Bread", Price = 2.99 },
                    new ProductEntity { Id = 3, Name = "Propane", Price = 30.99 }
                });
            modelBuilder.Entity<CustomerEntity>()
                .HasData(new CustomerEntity[]
                {
                    new CustomerEntity { Id = 1, FirstName = "Sam", LastName = "Alito" },
                    new CustomerEntity { Id = 2, FirstName = "Ruth", LastName = "Ginsburg" },
                    new CustomerEntity { Id = 3, FirstName = "Antonin", LastName = "Scalia"}
                });
            modelBuilder.Entity<InventoryEntity>()
                .HasData(
                    new InventoryEntity { Id = 1, Quantity = 150, StoreId = 1, ProductId = 1},
                    new InventoryEntity { Id = 2, Quantity = 200, StoreId = 1, ProductId = 2 },
                    new InventoryEntity { Id = 3, Quantity = 100, StoreId = 2, ProductId = 3 },
                    new InventoryEntity { Id = 4, Quantity = 150, StoreId = 2, ProductId = 1 },
                    new InventoryEntity { Id = 5, Quantity = 250, StoreId = 3, ProductId = 2 },
                    new InventoryEntity { Id = 6, Quantity = 100, StoreId = 3, ProductId = 3 }
                );
            modelBuilder.Entity<OrderEntity>()
                .HasData(new OrderEntity[]
                {
                    new OrderEntity { Id = 1, StoreId = 1, CustomerId = 1},
                    new OrderEntity { Id = 2, StoreId = 1, CustomerId = 2},
                    new OrderEntity { Id = 3, StoreId = 2, CustomerId = 3},
                    new OrderEntity { Id = 4, StoreId = 2, CustomerId = 1},
                    new OrderEntity { Id = 5, StoreId = 3, CustomerId = 2},
                    new OrderEntity { Id = 6, StoreId = 3, CustomerId = 3}
                });
            modelBuilder.Entity<OrderItemsEntity>()
                .HasData(new OrderItemsEntity[]
                {
                    new OrderItemsEntity {Id = 1, Quantity = 20, OrderId = 1,ProductId = 1},
                    new OrderItemsEntity {Id = 2, Quantity = 15, OrderId = 1,ProductId = 2},
                    new OrderItemsEntity {Id = 3, Quantity = 5,  OrderId = 2,ProductId = 3},
                    new OrderItemsEntity {Id = 4, Quantity = 30, OrderId = 6,ProductId = 1},
                    new OrderItemsEntity {Id = 5, Quantity = 10, OrderId = 3,ProductId = 2},
                    new OrderItemsEntity {Id = 6, Quantity = 25, OrderId = 3,ProductId = 3},
                    new OrderItemsEntity {Id = 7, Quantity = 20, OrderId = 4,ProductId = 1},
                    new OrderItemsEntity {Id = 8, Quantity = 15, OrderId = 5,ProductId = 2},
                    new OrderItemsEntity {Id = 9, Quantity = 5 , OrderId = 6,ProductId = 3}
                });
            modelBuilder.Entity<StoreEntity>()
                .HasData(new StoreEntity[]
                {
                    new StoreEntity { Id = 1, Name = "StoreA", Location = "Texas" },
                    new StoreEntity { Id = 2, Name = "StoreB", Location = "Arizona" },
                    new StoreEntity { Id = 3, Name = "StoreC", Location = "California"}
                });
        }
    }
}