using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class ProductOrder
    {
        public ProductOrder()
        {
            ProductOrderDetails = new HashSet<ProductOrderDetails>();
        }

        public int RequestId { get; set; }
        public decimal? ProductOrderAmount { get; set; }
        public DateTime? ProductOrderDate { get; set; }
        public int? ProductOrderQuantity { get; set; }

        public ICollection<ProductOrderDetails> ProductOrderDetails { get; set; }
    }
}
