using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Product names, suppliers, prices, and units in stock.
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        /// <summary>
        /// Number automatically assigned to new product.
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        /// <summary>
        /// Same entry as in Suppliers table.
        /// </summary>
        public int? SupplierId { get; set; }
        /// <summary>
        /// Same entry as in Categories table.
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// (e.g., 24-count case, 1-liter bottle).
        /// </summary>
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        /// <summary>
        /// Minimum units to maintain in stock.
        /// </summary>
        public short? ReorderLevel { get; set; }
        /// <summary>
        /// Yes means item is no longer available.
        /// </summary>
        public bool? Discontinued { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
