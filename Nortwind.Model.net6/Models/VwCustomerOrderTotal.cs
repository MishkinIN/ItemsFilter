using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwCustomerOrderTotal
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public double? TotalExtendedPrice { get; set; }
        public decimal? Freight { get; set; }
        public double? OrderTotal { get; set; }
    }
}
