using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementSystem.Entities.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [NotMapped]
        public List<Category> categoryList { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
