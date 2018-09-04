using Microsoft.EntityFrameworkCore;
using ProductDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDAL
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .Property(p => p.Timestamp)
                .IsRowVersion();
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }*/
    }
}
