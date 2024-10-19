namespace CoffeeManagementSystem.Entities.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string OrderState { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

    }


}
