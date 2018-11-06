using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class OrderDetails
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int? Quantity { get; set; }
        public decimal? OrderPrice { get; set; }

        public Book Book { get; set; }
    }
}
