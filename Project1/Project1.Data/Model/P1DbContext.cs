using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GetDate()")
                    .IsRequired();
            });

            modelBuilder.Entity<OrderItemsEntity>(entity =>
            {
                entity.ToTable("OrderItems");

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
        }
    }
}
