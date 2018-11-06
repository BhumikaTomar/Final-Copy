using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class Orders
    {
        public Orders()
        {
            Payment = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public decimal? OrderAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderQuantity { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
