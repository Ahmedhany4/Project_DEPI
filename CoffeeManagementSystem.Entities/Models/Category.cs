namespace CoffeeManagementSystem.Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        // image path for category
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }

}
