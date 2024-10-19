namespace CoffeeManagementSystem.Entities.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public List<Customer> customerList { get; set; }
        public List<Order> orderList { get; set; }
    }



}
