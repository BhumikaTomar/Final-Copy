using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class ProductOrderDetails
    {
        public int RequestId { get; set; }
        public int ProductOrderQuantity { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public ProductOrder Request { get; set; }
    }
}
