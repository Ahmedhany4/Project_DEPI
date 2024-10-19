using CoffeeManagementSystem.Entities;
using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContextFile
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    // Ensure the ProductImage property is mapped correctly
        //    modelBuilder.Entity<Product>()
        //        .Property(p => p.ProductImage)
        //        .HasColumnName("ProductImage") // Ensure the database column name matches
        //        .IsRequired(false); // Adjust based on your needs (true if it's a required field)

            
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Suppress warnings for invalid include path errors
        //    optionsBuilder.ConfigureWarnings(warnings =>
        //        warnings.Ignore(CoreEventId.InvalidIncludePathError));
        //}

        // DbSet properties for your entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet <Cart> Carts { get; set; }
    }
}
