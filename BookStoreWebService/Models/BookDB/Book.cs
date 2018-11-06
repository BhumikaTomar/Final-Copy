using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ReorderTransaction = new HashSet<ReorderTransaction>();
        }

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal? Price { get; set; }
        public int? BookQuantity { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string Image { get; set; }

        public Category Category { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<ReorderTransaction> ReorderTransaction { get; set; }
    }
}
