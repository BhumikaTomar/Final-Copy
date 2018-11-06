using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class Category
    {
        public Category()
        {
            Book = new HashSet<Book>();
            SubCategory = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public ICollection<Book> Book { get; set; }
        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
