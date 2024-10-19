using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagementSystem.Entities.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
