using System;
using System.Collections.Generic;

namespace BookStoreWebService.Models.BookDB
{
    public partial class Payment
    {
        public int InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }

        public Orders Order { get; set; }
    }
}
