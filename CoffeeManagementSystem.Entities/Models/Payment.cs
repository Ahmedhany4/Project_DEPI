namespace CoffeeManagementSystem.Entities.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

}
