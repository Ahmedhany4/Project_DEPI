namespace CoffeeManagementSystem.Entities.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? LoyaltyPoints { get; set; }
        public string SuggestFlavour { get; set; }
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }


}
