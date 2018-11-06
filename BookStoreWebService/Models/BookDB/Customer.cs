using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
