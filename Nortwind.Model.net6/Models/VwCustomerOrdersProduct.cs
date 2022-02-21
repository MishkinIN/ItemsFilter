using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwCustomerOrdersProduct
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
        public float? ExtendedPrice { get; set; }
    }
}
