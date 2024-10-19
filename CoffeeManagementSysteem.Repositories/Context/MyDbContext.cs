using CoffeeManagementSystem.Entities;
using CoffeeManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContextFile
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Product and Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // Navigation property in Product
                .WithMany(c => c.Products) // Navigation property in Category
                .HasForeignKey(p => p.CategoryId); // Foreign key property in Product

            // Ensure the ProductImage property is mapped correctly
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductImage)
                .HasColumnName("ProductImage") // Ensure the database column name matches
                .IsRequired(false); // Adjust based on your needs (true if it's a required field)
            modelBuilder.Entity<Customer>()
            .HasMany(c => c.Feedbacks)
            .WithOne(f => f.Customer)
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Set Delete behavior if necessary

            // Define the one-to-many relationship between Order and Feedback
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Feedbacks)
                .WithOne(f => f.Order)
                .HasForeignKey(f => f.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Suppress warnings for invalid include path errors
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.InvalidIncludePathError));
        }

        // DbSet properties for your entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
