using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class ReorderTransaction
    {
        public int RequestId { get; set; }
        public decimal? ProductOrderAmount { get; set; }
        public DateTime? ProductOrderDate { get; set; }
        public int? ProductOrderQuantity { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
