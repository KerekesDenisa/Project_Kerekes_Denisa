using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Kerekes_Denisa_SADE.Models;

namespace Project_Kerekes_Denisa_SADE.Data
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Chocolate> Chocolates { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SuppliedChocolate> SuppliedChocolates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Chocolate>().ToTable("Chocolate");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<SuppliedChocolate>().ToTable("SuppliedChocolate");
            modelBuilder.Entity<SuppliedChocolate>()
            .HasKey(c => new { c.ChocolateID, c.SupplierID });//configureaza cheia primara compusa
        }
    }
}
