namespace CoffeeManagementSystem.Entities.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ShiftSchedule { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

}
