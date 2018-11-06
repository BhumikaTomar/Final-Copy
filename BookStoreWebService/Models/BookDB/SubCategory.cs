using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryTitle { 

get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
