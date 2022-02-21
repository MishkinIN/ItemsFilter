using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Details on products, quantities, and prices for each order in the Orders table.
    /// </summary>
    public partial class OrderDetail
    {
        /// <summary>
        /// !NEW
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Same as Order ID in Orders table.
        /// </summary>
        public int? OrderId { get; set; }
        /// <summary>
        /// Same as Product ID in Products table.
        /// </summary>
        public int? ProductId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
